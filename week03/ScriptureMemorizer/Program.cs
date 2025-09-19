using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptureMemorizer
{
    /// <summary>
    /// Scripture Memorizer
    /// 
    /// Classes:
    /// - ScriptureReference: holds book, chapter and verse/range information
    /// - Word: represents a token (word) in the scripture and whether it is hidden
    /// - Scripture: holds the reference and the tokenized scripture text; handles hiding words
    /// - Program: runs the console application
    /// 
    /// Extra features (to exceed requirements):
    /// * Supports a small library of scriptures and will pick one at random.
    /// * If a file named "scriptures.txt" exists in the working directory it will be loaded (one scripture per line in the format: Reference|Text), e.g. "John 3:16|For God so loved the world...".
    /// * Hiding chooses only currently visible (not yet hidden) words to avoid pointless repeats.
    /// * Punctuation is preserved while hiding — only letters and digits are replaced with underscores so the number of underscores equals the number of letters/digits.
    /// * Correctly handles verse ranges in references (e.g. "Proverbs 3:5-6") via multiple constructors and a parsing constructor.
    /// * Hides a random small number of words each enter press (1–3) to make memorization gradual.
    ///
    /// </summary>

    public class ScriptureReference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int? _endVerse; // null if single verse

        public string Book => _book;
        public int Chapter => _chapter;
        public int StartVerse => _startVerse;
        public int? EndVerse => _endVerse;

        // Constructor for single verse
        public ScriptureReference(string book, int chapter, int verse)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _chapter = chapter;
            _startVerse = verse;
            _endVerse = null;
        }

        // Constructor for range
        public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        // Parsing constructor from strings 
        public ScriptureReference(string referenceText)
        {
            if (string.IsNullOrWhiteSpace(referenceText)) throw new ArgumentNullException(nameof(referenceText));

            // Regex breakdown:
            // (?<book>.*?)   - non-greedy capture of book name (can contain numbers and spaces, e.g., "1 John")
            // \s+(?<chapter>\d+) - chapter number
            // :(?<verses>\d+(?:-\d+)?) - verses either single or range
            var match = Regex.Match(referenceText.Trim(), @"^(?<book>.+?)\s+(?<chapter>\d+):(?<verses>\d+(?:-\d+)?)$");
            if (!match.Success)
            {
                throw new ArgumentException($"Invalid scripture reference format: '{referenceText}'. Expected formats like 'John 3:16' or 'Proverbs 3:5-6'.");
            }

            _book = match.Groups["book"].Value.Trim();
            _chapter = int.Parse(match.Groups["chapter"].Value);
            var verses = match.Groups["verses"].Value;
            if (verses.Contains("-"))
            {
                var parts = verses.Split('-');
                _startVerse = int.Parse(parts[0]);
                _endVerse = int.Parse(parts[1]);
            }
            else
            {
                _startVerse = int.Parse(verses);
                _endVerse = null;
            }
        }

        public override string ToString()
        {
            if (_endVerse.HasValue)
            {
                return $"{_book} {_chapter}:{_startVerse}-{_endVerse.Value}";
            }
            else
            {
                return $"{_book} {_chapter}:{_startVerse}";
            }
        }
    }

    public class Word
    {
        private string _text;
        private bool _hidden;

        public Word(string text)
        {
            _text = text ?? string.Empty;
            _hidden = false;
        }

        /// <summary>
        /// Returns true if the token contains at least one letter or digit.
        /// </summary>
        public bool ContainsLetterOrDigit => _text.Any(char.IsLetterOrDigit);

        public bool IsHidden => _hidden;

        /// <summary>
        /// Mark the word as hidden. Returns true if this call changed the state (was visible and became hidden).
        /// </summary>
        public bool Hide()
        {
            if (!ContainsLetterOrDigit) return false; // punctuation-only tokens are not hideable
            if (_hidden) return false;
            _hidden = true;
            return true;
        }

        /// <summary>
        /// Display text depending on hidden state. When hidden, letters and digits are replaced by underscores '_' , punctuation remains.
        /// That ensures the number of underscores equals the number of letters/digits in the word.
        /// </summary>
        public string DisplayText
        {
            get
            {
                if (!_hidden) return _text;
                // Replace letters and digits with underscores, keep other characters (punctuation) as-is
                var chars = _text.Select(c => char.IsLetterOrDigit(c) ? '_' : c).ToArray();
                return new string(chars);
            }
        }

        public override string ToString() => DisplayText;
    }

    public class Scripture
    {
        private ScriptureReference _reference;

        // Tokens: either whitespace tokens (Text) or Word tokens (Word)
        private class Token
        {
            public bool IsWord { get; }
            public string Text { get; } // original text for whitespace tokens or for reference
            public Word Word { get; }

            public Token(string text, bool isWord)
            {
                IsWord = isWord;
                Text = text;
                if (isWord) Word = new Word(text);
            }

            public string Display() => IsWord ? Word.DisplayText : Text;
            public bool Hide() => IsWord && Word.Hide();
            public bool IsHidden => IsWord && Word.IsHidden;
            public bool IsHideable => IsWord && Word.ContainsLetterOrDigit;
        }

        private List<Token> _tokens = new List<Token>();
        private static Random _random = new Random();

        public ScriptureReference Reference => _reference;

        /// <summary>
        /// Construct a scripture from a reference and the full text. The text may include multiple verses.
        /// </summary>
        public Scripture(ScriptureReference reference, string text)
        {
            _reference = reference ?? throw new ArgumentNullException(nameof(reference));
            if (text == null) text = string.Empty;

            // Tokenize into whitespace and non-whitespace sequences to preserve spaces and punctuation placement
            var matches = Regex.Matches(text, "(\\s+)|(\\S+)");
            foreach (Match m in matches)
            {
                if (m.Groups[1].Success)
                {
                    // whitespace
                    _tokens.Add(new Token(m.Groups[1].Value, false));
                }
                else
                {
                    _tokens.Add(new Token(m.Groups[2].Value, true));
                }
            }
        }

        /// <summary>
        /// Returns the scripture text with hidden words replaced by underscores (punctuation preserved).
        /// </summary>
        public string DisplayText()
        {
            return string.Concat(_tokens.Select(t => t.Display()));
        }

        /// <summary>
        /// Returns true when all hideable words are hidden.
        /// </summary>
        public bool AllWordsHidden()
        {
            return !_tokens.Any(t => t.IsHideable && !t.IsHidden);
        }

        /// <summary>
        /// Hide up to count random visible words. Returns the number of words actually hidden.
        /// The selection chooses only currently visible (not yet hidden) words to avoid pointless repeats.
        /// </summary>
        public int HideRandomVisibleWords(int count)
        {
            if (count <= 0) return 0;
            var candidates = _tokens
                .Select((t, i) => new { Token = t, Index = i })
                .Where(x => x.Token.IsHideable && !x.Token.IsHidden)
                .ToList();

            if (!candidates.Any()) return 0;

            // Shuffle candidate indices
            var shuffled = candidates.OrderBy(x => _random.Next()).ToList();
            int toHide = Math.Min(count, shuffled.Count);
            int hidden = 0;
            for (int i = 0; i < toHide; i++)
            {
                if (shuffled[i].Token.Hide()) hidden++;
            }
            return hidden;
        }

        /// <summary>
        /// Hide a small random number (1 to 3) of words. Returns number of words hidden.
        /// </summary>
        public int HideAFewWords()
        {
            int n = _random.Next(1, 4); // 1,2 or 3
            return HideRandomVisibleWords(n);
        }
    }

    public class Program
    {
        /*
         * Extra features:
         * 1) Scripture library - there is a built-in list of scriptures and the program will randomly select one to present.
         * 2) File input - if a file named "scriptures.txt" is present it will load additional scriptures from it. The file format is one scripture per line using: Reference|Text (pipe separated). Example:
         *      John 3:16|For God so loved the world, that he gave his only begotten Son, that whoever believes in him should not perish but have eternal life.
         * 3) Hiding logic avoids re-hiding already hidden words (this makes memorization meaningful).
         * 4) Punctuation-safe hiding: only letters and digits are replaced with underscores; punctuation and apostrophes remain visible in place.
         * 5) Word/token preservation: whitespace and punctuation are preserved so the display format follows the original scripture text.
         */

        static void Main(string[] args)
        {
            var library = LoadLibrary();
            var rnd = new Random();
            var scripture = library[rnd.Next(library.Count)];

            // Initial display
            Console.Clear();
            Console.WriteLine($"{scripture.Reference}\n{scripture.DisplayText()}");

            while (true)
            {
                if (scripture.AllWordsHidden())
                {
                    // Final display (all words hidden) - show and exit
                    Console.Clear();
                    Console.WriteLine($"{scripture.Reference}\n{scripture.DisplayText()}");
                    Console.WriteLine();
                    Console.WriteLine("All words are now hidden. Press Enter to exit.");
                    Console.ReadLine();
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter to hide a few words, or type 'quit' and press Enter to exit.");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    // Exit program
                    break;
                }

                // Hide a few words and re-display
                scripture.HideAFewWords();
                Console.Clear();
                Console.WriteLine($"{scripture.Reference}\n{scripture.DisplayText()}");
            }
        }

        private static List<Scripture> LoadLibrary()
        {
            var list = new List<Scripture>();

            // Built-in scriptures
            list.Add(new Scripture(new ScriptureReference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whoever believes in him should not perish but have eternal life."));

            list.Add(new Scripture(new ScriptureReference("Proverbs", 3, 5, 6),
                "Trust in the LORD with all your heart and do not rely on your own understanding; in all your ways acknowledge him, and he will make your paths straight."));

            list.Add(new Scripture(new ScriptureReference("Psalm", 23, 1, 4),
                "The LORD is my shepherd; I have everything I need. He lets me rest in green meadows; he leads me beside peaceful streams. He renews my strength. He guides me along right paths, bringing honor to his name."));

            list.Add(new Scripture(new ScriptureReference("Romans", 8, 28),
                "And we know that all things work together for good for those who love God, who are called according to his purpose."));

            list.Add(new Scripture(new ScriptureReference("Philippians", 4, 13),
                "I can do all things through him who strengthens me."));

            // Attempt to load additional scriptures from a file (optional feature)
            try
            {
                var file = "scriptures.txt";
                if (System.IO.File.Exists(file))
                {
                    var lines = System.IO.File.ReadAllLines(file);
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        // Expected format: Reference|Text
                        var parts = line.Split(new char[] { '|' }, 2);
                        if (parts.Length < 2) continue;
                        try
                        {
                            var reference = new ScriptureReference(parts[0].Trim());
                            var text = parts[1].Trim();
                            list.Add(new Scripture(reference, text));
                        }
                        catch
                        {
                            // ignore invalid lines
                        }
                    }
                }
            }
            catch
            {
                // If any IO error occurs, ignore and proceed with built-in list
            }

            return list;
        }
    }
}