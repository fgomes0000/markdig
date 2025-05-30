
// --------------------------------
//          CommonMarkSpecs
// --------------------------------

using System;
using NUnit.Framework;

namespace Markdig.Tests.Specs.CommonMarkSpecs
{
    [TestFixture]
    public class TestPreliminariesTabs
    {
        // ---
        // title: CommonMark Spec
        // author: John MacFarlane
        // version: '0.31.2'
        // date: '2024-01-28'
        // license: '[CC-BY-SA 4.0](https://creativecommons.org/licenses/by-sa/4.0/)'
        // ...
        // 
        // # Introduction
        // 
        // ## What is Markdown?
        // 
        // Markdown is a plain text format for writing structured documents,
        // based on conventions for indicating formatting in email
        // and usenet posts.  It was developed by John Gruber (with
        // help from Aaron Swartz) and released in 2004 in the form of a
        // [syntax description](https://daringfireball.net/projects/markdown/syntax)
        // and a Perl script (`Markdown.pl`) for converting Markdown to
        // HTML.  In the next decade, dozens of implementations were
        // developed in many languages.  Some extended the original
        // Markdown syntax with conventions for footnotes, tables, and
        // other document elements.  Some allowed Markdown documents to be
        // rendered in formats other than HTML.  Websites like Reddit,
        // StackOverflow, and GitHub had millions of people using Markdown.
        // And Markdown started to be used beyond the web, to author books,
        // articles, slide shows, letters, and lecture notes.
        // 
        // What distinguishes Markdown from many other lightweight markup
        // syntaxes, which are often easier to write, is its readability.
        // As Gruber writes:
        // 
        // > The overriding design goal for Markdown's formatting syntax is
        // > to make it as readable as possible. The idea is that a
        // > Markdown-formatted document should be publishable as-is, as
        // > plain text, without looking like it's been marked up with tags
        // > or formatting instructions.
        // > (<https://daringfireball.net/projects/markdown/>)
        // 
        // The point can be illustrated by comparing a sample of
        // [AsciiDoc](https://asciidoc.org/) with
        // an equivalent sample of Markdown.  Here is a sample of
        // AsciiDoc from the AsciiDoc manual:
        // 
        // ```
        // 1. List item one.
        // +
        // List item one continued with a second paragraph followed by an
        // Indented block.
        // +
        // .................
        // $ ls *.sh
        // $ mv *.sh ~/tmp
        // .................
        // +
        // List item continued with a third paragraph.
        // 
        // 2. List item two continued with an open block.
        // +
        // --
        // This paragraph is part of the preceding list item.
        // 
        // a. This list is nested and does not require explicit item
        // continuation.
        // +
        // This paragraph is part of the preceding list item.
        // 
        // b. List item b.
        // 
        // This paragraph belongs to item two of the outer list.
        // --
        // ```
        // 
        // And here is the equivalent in Markdown:
        // ```
        // 1.  List item one.
        // 
        //     List item one continued with a second paragraph followed by an
        //     Indented block.
        // 
        //         $ ls *.sh
        //         $ mv *.sh ~/tmp
        // 
        //     List item continued with a third paragraph.
        // 
        // 2.  List item two continued with an open block.
        // 
        //     This paragraph is part of the preceding list item.
        // 
        //     1. This list is nested and does not require explicit item continuation.
        // 
        //        This paragraph is part of the preceding list item.
        // 
        //     2. List item b.
        // 
        //     This paragraph belongs to item two of the outer list.
        // ```
        // 
        // The AsciiDoc version is, arguably, easier to write. You don't need
        // to worry about indentation.  But the Markdown version is much easier
        // to read.  The nesting of list items is apparent to the eye in the
        // source, not just in the processed document.
        // 
        // ## Why is a spec needed?
        // 
        // John Gruber's [canonical description of Markdown's
        // syntax](https://daringfireball.net/projects/markdown/syntax)
        // does not specify the syntax unambiguously.  Here are some examples of
        // questions it does not answer:
        // 
        // 1.  How much indentation is needed for a sublist?  The spec says that
        //     continuation paragraphs need to be indented four spaces, but is
        //     not fully explicit about sublists.  It is natural to think that
        //     they, too, must be indented four spaces, but `Markdown.pl` does
        //     not require that.  This is hardly a "corner case," and divergences
        //     between implementations on this issue often lead to surprises for
        //     users in real documents. (See [this comment by John
        //     Gruber](https://web.archive.org/web/20170611172104/http://article.gmane.org/gmane.text.markdown.general/1997).)
        // 
        // 2.  Is a blank line needed before a block quote or heading?
        //     Most implementations do not require the blank line.  However,
        //     this can lead to unexpected results in hard-wrapped text, and
        //     also to ambiguities in parsing (note that some implementations
        //     put the heading inside the blockquote, while others do not).
        //     (John Gruber has also spoken [in favor of requiring the blank
        //     lines](https://web.archive.org/web/20170611172104/http://article.gmane.org/gmane.text.markdown.general/2146).)
        // 
        // 3.  Is a blank line needed before an indented code block?
        //     (`Markdown.pl` requires it, but this is not mentioned in the
        //     documentation, and some implementations do not require it.)
        // 
        //     ``` markdown
        //     paragraph
        //         code?
        //     ```
        // 
        // 4.  What is the exact rule for determining when list items get
        //     wrapped in `<p>` tags?  Can a list be partially "loose" and partially
        //     "tight"?  What should we do with a list like this?
        // 
        //     ``` markdown
        //     1. one
        // 
        //     2. two
        //     3. three
        //     ```
        // 
        //     Or this?
        // 
        //     ``` markdown
        //     1.  one
        //         - a
        // 
        //         - b
        //     2.  two
        //     ```
        // 
        //     (There are some relevant comments by John Gruber
        //     [here](https://web.archive.org/web/20170611172104/http://article.gmane.org/gmane.text.markdown.general/2554).)
        // 
        // 5.  Can list markers be indented?  Can ordered list markers be right-aligned?
        // 
        //     ``` markdown
        //      8. item 1
        //      9. item 2
        //     10. item 2a
        //     ```
        // 
        // 6.  Is this one list with a thematic break in its second item,
        //     or two lists separated by a thematic break?
        // 
        //     ``` markdown
        //     * a
        //     * * * * *
        //     * b
        //     ```
        // 
        // 7.  When list markers change from numbers to bullets, do we have
        //     two lists or one?  (The Markdown syntax description suggests two,
        //     but the perl scripts and many other implementations produce one.)
        // 
        //     ``` markdown
        //     1. fee
        //     2. fie
        //     -  foe
        //     -  fum
        //     ```
        // 
        // 8.  What are the precedence rules for the markers of inline structure?
        //     For example, is the following a valid link, or does the code span
        //     take precedence ?
        // 
        //     ``` markdown
        //     [a backtick (`)](/url) and [another backtick (`)](/url).
        //     ```
        // 
        // 9.  What are the precedence rules for markers of emphasis and strong
        //     emphasis?  For example, how should the following be parsed?
        // 
        //     ``` markdown
        //     *foo *bar* baz*
        //     ```
        // 
        // 10. What are the precedence rules between block-level and inline-level
        //     structure?  For example, how should the following be parsed?
        // 
        //     ``` markdown
        //     - `a long code span can contain a hyphen like this
        //       - and it can screw things up`
        //     ```
        // 
        // 11. Can list items include section headings?  (`Markdown.pl` does not
        //     allow this, but does allow blockquotes to include headings.)
        // 
        //     ``` markdown
        //     - # Heading
        //     ```
        // 
        // 12. Can list items be empty?
        // 
        //     ``` markdown
        //     * a
        //     *
        //     * b
        //     ```
        // 
        // 13. Can link references be defined inside block quotes or list items?
        // 
        //     ``` markdown
        //     > Blockquote [foo].
        //     >
        //     > [foo]: /url
        //     ```
        // 
        // 14. If there are multiple definitions for the same reference, which takes
        //     precedence?
        // 
        //     ``` markdown
        //     [foo]: /url1
        //     [foo]: /url2
        // 
        //     [foo][]
        //     ```
        // 
        // In the absence of a spec, early implementers consulted `Markdown.pl`
        // to resolve these ambiguities.  But `Markdown.pl` was quite buggy, and
        // gave manifestly bad results in many cases, so it was not a
        // satisfactory replacement for a spec.
        // 
        // Because there is no unambiguous spec, implementations have diverged
        // considerably.  As a result, users are often surprised to find that
        // a document that renders one way on one system (say, a GitHub wiki)
        // renders differently on another (say, converting to docbook using
        // pandoc).  To make matters worse, because nothing in Markdown counts
        // as a "syntax error," the divergence often isn't discovered right away.
        // 
        // ## About this document
        // 
        // This document attempts to specify Markdown syntax unambiguously.
        // It contains many examples with side-by-side Markdown and
        // HTML.  These are intended to double as conformance tests.  An
        // accompanying script `spec_tests.py` can be used to run the tests
        // against any Markdown program:
        // 
        //     python test/spec_tests.py --spec spec.txt --program PROGRAM
        // 
        // Since this document describes how Markdown is to be parsed into
        // an abstract syntax tree, it would have made sense to use an abstract
        // representation of the syntax tree instead of HTML.  But HTML is capable
        // of representing the structural distinctions we need to make, and the
        // choice of HTML for the tests makes it possible to run the tests against
        // an implementation without writing an abstract syntax tree renderer.
        // 
        // Note that not every feature of the HTML samples is mandated by
        // the spec.  For example, the spec says what counts as a link
        // destination, but it doesn't mandate that non-ASCII characters in
        // the URL be percent-encoded.  To use the automatic tests,
        // implementers will need to provide a renderer that conforms to
        // the expectations of the spec examples (percent-encoding
        // non-ASCII characters in URLs).  But a conforming implementation
        // can use a different renderer and may choose not to
        // percent-encode non-ASCII characters in URLs.
        // 
        // This document is generated from a text file, `spec.txt`, written
        // in Markdown with a small extension for the side-by-side tests.
        // The script `tools/makespec.py` can be used to convert `spec.txt` into
        // HTML or CommonMark (which can then be converted into other formats).
        // 
        // In the examples, the `→` character is used to represent tabs.
        // 
        // # Preliminaries
        // 
        // ## Characters and lines
        // 
        // Any sequence of [characters] is a valid CommonMark
        // document.
        // 
        // A [character](@) is a Unicode code point.  Although some
        // code points (for example, combining accents) do not correspond to
        // characters in an intuitive sense, all code points count as characters
        // for purposes of this spec.
        // 
        // This spec does not specify an encoding; it thinks of lines as composed
        // of [characters] rather than bytes.  A conforming parser may be limited
        // to a certain encoding.
        // 
        // A [line](@) is a sequence of zero or more [characters]
        // other than line feed (`U+000A`) or carriage return (`U+000D`),
        // followed by a [line ending] or by the end of file.
        // 
        // A [line ending](@) is a line feed (`U+000A`), a carriage return
        // (`U+000D`) not followed by a line feed, or a carriage return and a
        // following line feed.
        // 
        // A line containing no characters, or a line containing only spaces
        // (`U+0020`) or tabs (`U+0009`), is called a [blank line](@).
        // 
        // The following definitions of character classes will be used in this spec:
        // 
        // A [Unicode whitespace character](@) is a character in the Unicode `Zs` general
        // category, or a tab (`U+0009`), line feed (`U+000A`), form feed (`U+000C`), or
        // carriage return (`U+000D`).
        // 
        // [Unicode whitespace](@) is a sequence of one or more
        // [Unicode whitespace characters].
        // 
        // A [tab](@) is `U+0009`.
        // 
        // A [space](@) is `U+0020`.
        // 
        // An [ASCII control character](@) is a character between `U+0000–1F` (both
        // including) or `U+007F`.
        // 
        // An [ASCII punctuation character](@)
        // is `!`, `"`, `#`, `$`, `%`, `&`, `'`, `(`, `)`,
        // `*`, `+`, `,`, `-`, `.`, `/` (U+0021–2F), 
        // `:`, `;`, `<`, `=`, `>`, `?`, `@` (U+003A–0040),
        // `[`, `\`, `]`, `^`, `_`, `` ` `` (U+005B–0060), 
        // `{`, `|`, `}`, or `~` (U+007B–007E).
        // 
        // A [Unicode punctuation character](@) is a character in the Unicode `P`
        // (puncuation) or `S` (symbol) general categories.
        // 
        // ## Tabs
        // 
        // Tabs in lines are not expanded to [spaces].  However,
        // in contexts where spaces help to define block structure,
        // tabs behave as if they were replaced by spaces with a tab stop
        // of 4 characters.
        // 
        // Thus, for example, a tab can be used instead of four spaces
        // in an indented code block.  (Note, however, that internal
        // tabs are passed through as literal tabs, not expanded to
        // spaces.)
        [Test]
        public void PreliminariesTabs_Example001()
        {
            // Example 1
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //     →foo→baz→→bim
            //
            // Should be rendered as:
            //     <pre><code>foo→baz→→bim
            //     </code></pre>

            TestParser.TestSpec("\tfoo\tbaz\t\tbim", "<pre><code>foo\tbaz\t\tbim\n</code></pre>", "", context: "Example 1\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example002()
        {
            // Example 2
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //       →foo→baz→→bim
            //
            // Should be rendered as:
            //     <pre><code>foo→baz→→bim
            //     </code></pre>

            TestParser.TestSpec("  \tfoo\tbaz\t\tbim", "<pre><code>foo\tbaz\t\tbim\n</code></pre>", "", context: "Example 2\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example003()
        {
            // Example 3
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //         a→a
            //         ὐ→a
            //
            // Should be rendered as:
            //     <pre><code>a→a
            //     ὐ→a
            //     </code></pre>

            TestParser.TestSpec("    a\ta\n    ὐ\ta", "<pre><code>a\ta\nὐ\ta\n</code></pre>", "", context: "Example 3\nSection Preliminaries / Tabs\n");
        }

        // In the following example, a continuation paragraph of a list
        // item is indented with a tab; this has exactly the same effect
        // as indentation with four spaces would:
        [Test]
        public void PreliminariesTabs_Example004()
        {
            // Example 4
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //       - foo
            //     
            //     →bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("  - foo\n\n\tbar", "<ul>\n<li>\n<p>foo</p>\n<p>bar</p>\n</li>\n</ul>", "", context: "Example 4\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example005()
        {
            // Example 5
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //     - foo
            //     
            //     →→bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <pre><code>  bar
            //     </code></pre>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- foo\n\n\t\tbar", "<ul>\n<li>\n<p>foo</p>\n<pre><code>  bar\n</code></pre>\n</li>\n</ul>", "", context: "Example 5\nSection Preliminaries / Tabs\n");
        }

        // Normally the `>` that begins a block quote may be followed
        // optionally by a space, which is not considered part of the
        // content.  In the following case `>` is followed by a tab,
        // which is treated as if it were expanded into three spaces.
        // Since one of these spaces is considered part of the
        // delimiter, `foo` is considered to be indented six spaces
        // inside the block quote context, so we get an indented
        // code block starting with two spaces.
        [Test]
        public void PreliminariesTabs_Example006()
        {
            // Example 6
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //     >→→foo
            //
            // Should be rendered as:
            //     <blockquote>
            //     <pre><code>  foo
            //     </code></pre>
            //     </blockquote>

            TestParser.TestSpec(">\t\tfoo", "<blockquote>\n<pre><code>  foo\n</code></pre>\n</blockquote>", "", context: "Example 6\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example007()
        {
            // Example 7
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //     -→→foo
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <pre><code>  foo
            //     </code></pre>
            //     </li>
            //     </ul>

            TestParser.TestSpec("-\t\tfoo", "<ul>\n<li>\n<pre><code>  foo\n</code></pre>\n</li>\n</ul>", "", context: "Example 7\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example008()
        {
            // Example 8
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //         foo
            //     →bar
            //
            // Should be rendered as:
            //     <pre><code>foo
            //     bar
            //     </code></pre>

            TestParser.TestSpec("    foo\n\tbar", "<pre><code>foo\nbar\n</code></pre>", "", context: "Example 8\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example009()
        {
            // Example 9
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //      - foo
            //        - bar
            //     → - baz
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo
            //     <ul>
            //     <li>bar
            //     <ul>
            //     <li>baz</li>
            //     </ul>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>

            TestParser.TestSpec(" - foo\n   - bar\n\t - baz", "<ul>\n<li>foo\n<ul>\n<li>bar\n<ul>\n<li>baz</li>\n</ul>\n</li>\n</ul>\n</li>\n</ul>", "", context: "Example 9\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example010()
        {
            // Example 10
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //     #→Foo
            //
            // Should be rendered as:
            //     <h1>Foo</h1>

            TestParser.TestSpec("#\tFoo", "<h1>Foo</h1>", "", context: "Example 10\nSection Preliminaries / Tabs\n");
        }

        [Test]
        public void PreliminariesTabs_Example011()
        {
            // Example 11
            // Section: Preliminaries / Tabs
            //
            // The following Markdown:
            //     *→*→*→
            //
            // Should be rendered as:
            //     <hr />

            TestParser.TestSpec("*\t*\t*\t", "<hr />", "", context: "Example 11\nSection Preliminaries / Tabs\n");
        }
    }

    [TestFixture]
    public class TestPreliminariesBackslashEscapes
    {
        // ## Insecure characters
        // 
        // For security reasons, the Unicode character `U+0000` must be replaced
        // with the REPLACEMENT CHARACTER (`U+FFFD`).
        // 
        // 
        // ## Backslash escapes
        // 
        // Any ASCII punctuation character may be backslash-escaped:
        [Test]
        public void PreliminariesBackslashEscapes_Example012()
        {
            // Example 12
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     \!\"\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~
            //
            // Should be rendered as:
            //     <p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\]^_`{|}~</p>

            TestParser.TestSpec("\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\`\\{\\|\\}\\~", "<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\\]^_`{|}~</p>", "", context: "Example 12\nSection Preliminaries / Backslash escapes\n");
        }

        // Backslashes before other characters are treated as literal
        // backslashes:
        [Test]
        public void PreliminariesBackslashEscapes_Example013()
        {
            // Example 13
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     \→\A\a\ \3\φ\«
            //
            // Should be rendered as:
            //     <p>\→\A\a\ \3\φ\«</p>

            TestParser.TestSpec("\\\t\\A\\a\\ \\3\\φ\\«", "<p>\\\t\\A\\a\\ \\3\\φ\\«</p>", "", context: "Example 13\nSection Preliminaries / Backslash escapes\n");
        }

        // Escaped characters are treated as regular characters and do
        // not have their usual Markdown meanings:
        [Test]
        public void PreliminariesBackslashEscapes_Example014()
        {
            // Example 14
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     \*not emphasized*
            //     \<br/> not a tag
            //     \[not a link](/foo)
            //     \`not code`
            //     1\. not a list
            //     \* not a list
            //     \# not a heading
            //     \[foo]: /url "not a reference"
            //     \&ouml; not a character entity
            //
            // Should be rendered as:
            //     <p>*not emphasized*
            //     &lt;br/&gt; not a tag
            //     [not a link](/foo)
            //     `not code`
            //     1. not a list
            //     * not a list
            //     # not a heading
            //     [foo]: /url &quot;not a reference&quot;
            //     &amp;ouml; not a character entity</p>

            TestParser.TestSpec("\\*not emphasized*\n\\<br/> not a tag\n\\[not a link](/foo)\n\\`not code`\n1\\. not a list\n\\* not a list\n\\# not a heading\n\\[foo]: /url \"not a reference\"\n\\&ouml; not a character entity", "<p>*not emphasized*\n&lt;br/&gt; not a tag\n[not a link](/foo)\n`not code`\n1. not a list\n* not a list\n# not a heading\n[foo]: /url &quot;not a reference&quot;\n&amp;ouml; not a character entity</p>", "", context: "Example 14\nSection Preliminaries / Backslash escapes\n");
        }

        // If a backslash is itself escaped, the following character is not:
        [Test]
        public void PreliminariesBackslashEscapes_Example015()
        {
            // Example 15
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     \\*emphasis*
            //
            // Should be rendered as:
            //     <p>\<em>emphasis</em></p>

            TestParser.TestSpec("\\\\*emphasis*", "<p>\\<em>emphasis</em></p>", "", context: "Example 15\nSection Preliminaries / Backslash escapes\n");
        }

        // A backslash at the end of the line is a [hard line break]:
        [Test]
        public void PreliminariesBackslashEscapes_Example016()
        {
            // Example 16
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     foo\
            //     bar
            //
            // Should be rendered as:
            //     <p>foo<br />
            //     bar</p>

            TestParser.TestSpec("foo\\\nbar", "<p>foo<br />\nbar</p>", "", context: "Example 16\nSection Preliminaries / Backslash escapes\n");
        }

        // Backslash escapes do not work in code blocks, code spans, autolinks, or
        // raw HTML:
        [Test]
        public void PreliminariesBackslashEscapes_Example017()
        {
            // Example 17
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     `` \[\` ``
            //
            // Should be rendered as:
            //     <p><code>\[\`</code></p>

            TestParser.TestSpec("`` \\[\\` ``", "<p><code>\\[\\`</code></p>", "", context: "Example 17\nSection Preliminaries / Backslash escapes\n");
        }

        [Test]
        public void PreliminariesBackslashEscapes_Example018()
        {
            // Example 18
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //         \[\]
            //
            // Should be rendered as:
            //     <pre><code>\[\]
            //     </code></pre>

            TestParser.TestSpec("    \\[\\]", "<pre><code>\\[\\]\n</code></pre>", "", context: "Example 18\nSection Preliminaries / Backslash escapes\n");
        }

        [Test]
        public void PreliminariesBackslashEscapes_Example019()
        {
            // Example 19
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     ~~~
            //     \[\]
            //     ~~~
            //
            // Should be rendered as:
            //     <pre><code>\[\]
            //     </code></pre>

            TestParser.TestSpec("~~~\n\\[\\]\n~~~", "<pre><code>\\[\\]\n</code></pre>", "", context: "Example 19\nSection Preliminaries / Backslash escapes\n");
        }

        [Test]
        public void PreliminariesBackslashEscapes_Example020()
        {
            // Example 20
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     <https://example.com?find=\*>
            //
            // Should be rendered as:
            //     <p><a href="https://example.com?find=%5C*">https://example.com?find=\*</a></p>

            TestParser.TestSpec("<https://example.com?find=\\*>", "<p><a href=\"https://example.com?find=%5C*\">https://example.com?find=\\*</a></p>", "", context: "Example 20\nSection Preliminaries / Backslash escapes\n");
        }

        [Test]
        public void PreliminariesBackslashEscapes_Example021()
        {
            // Example 21
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     <a href="/bar\/)">
            //
            // Should be rendered as:
            //     <a href="/bar\/)">

            TestParser.TestSpec("<a href=\"/bar\\/)\">", "<a href=\"/bar\\/)\">", "", context: "Example 21\nSection Preliminaries / Backslash escapes\n");
        }

        // But they work in all other contexts, including URLs and link titles,
        // link references, and [info strings] in [fenced code blocks]:
        [Test]
        public void PreliminariesBackslashEscapes_Example022()
        {
            // Example 22
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     [foo](/bar\* "ti\*tle")
            //
            // Should be rendered as:
            //     <p><a href="/bar*" title="ti*tle">foo</a></p>

            TestParser.TestSpec("[foo](/bar\\* \"ti\\*tle\")", "<p><a href=\"/bar*\" title=\"ti*tle\">foo</a></p>", "", context: "Example 22\nSection Preliminaries / Backslash escapes\n");
        }

        [Test]
        public void PreliminariesBackslashEscapes_Example023()
        {
            // Example 23
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     [foo]
            //     
            //     [foo]: /bar\* "ti\*tle"
            //
            // Should be rendered as:
            //     <p><a href="/bar*" title="ti*tle">foo</a></p>

            TestParser.TestSpec("[foo]\n\n[foo]: /bar\\* \"ti\\*tle\"", "<p><a href=\"/bar*\" title=\"ti*tle\">foo</a></p>", "", context: "Example 23\nSection Preliminaries / Backslash escapes\n");
        }

        [Test]
        public void PreliminariesBackslashEscapes_Example024()
        {
            // Example 24
            // Section: Preliminaries / Backslash escapes
            //
            // The following Markdown:
            //     ``` foo\+bar
            //     foo
            //     ```
            //
            // Should be rendered as:
            //     <pre><code class="language-foo+bar">foo
            //     </code></pre>

            TestParser.TestSpec("``` foo\\+bar\nfoo\n```", "<pre><code class=\"language-foo+bar\">foo\n</code></pre>", "", context: "Example 24\nSection Preliminaries / Backslash escapes\n");
        }
    }

    [TestFixture]
    public class TestPreliminariesEntityAndNumericCharacterReferences
    {
        // ## Entity and numeric character references
        // 
        // Valid HTML entity references and numeric character references
        // can be used in place of the corresponding Unicode character,
        // with the following exceptions:
        // 
        // - Entity and character references are not recognized in code
        //   blocks and code spans.
        // 
        // - Entity and character references cannot stand in place of
        //   special characters that define structural elements in
        //   CommonMark.  For example, although `&#42;` can be used
        //   in place of a literal `*` character, `&#42;` cannot replace
        //   `*` in emphasis delimiters, bullet list markers, or thematic
        //   breaks.
        // 
        // Conforming CommonMark parsers need not store information about
        // whether a particular character was represented in the source
        // using a Unicode character or an entity reference.
        // 
        // [Entity references](@) consist of `&` + any of the valid
        // HTML5 entity names + `;`. The
        // document <https://html.spec.whatwg.org/entities.json>
        // is used as an authoritative source for the valid entity
        // references and their corresponding code points.
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example025()
        {
            // Example 25
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &nbsp; &amp; &copy; &AElig; &Dcaron;
            //     &frac34; &HilbertSpace; &DifferentialD;
            //     &ClockwiseContourIntegral; &ngE;
            //
            // Should be rendered as:
            //     <p>  &amp; © Æ Ď
            //     ¾ ℋ ⅆ
            //     ∲ ≧̸</p>

            TestParser.TestSpec("&nbsp; &amp; &copy; &AElig; &Dcaron;\n&frac34; &HilbertSpace; &DifferentialD;\n&ClockwiseContourIntegral; &ngE;", "<p>  &amp; © Æ Ď\n¾ ℋ ⅆ\n∲ ≧̸</p>", "", context: "Example 25\nSection Preliminaries / Entity and numeric character references\n");
        }

        // [Decimal numeric character
        // references](@)
        // consist of `&#` + a string of 1--7 arabic digits + `;`. A
        // numeric character reference is parsed as the corresponding
        // Unicode character. Invalid Unicode code points will be replaced by
        // the REPLACEMENT CHARACTER (`U+FFFD`).  For security reasons,
        // the code point `U+0000` will also be replaced by `U+FFFD`.
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example026()
        {
            // Example 26
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &#35; &#1234; &#992; &#0;
            //
            // Should be rendered as:
            //     <p># Ӓ Ϡ �</p>

            TestParser.TestSpec("&#35; &#1234; &#992; &#0;", "<p># Ӓ Ϡ �</p>", "", context: "Example 26\nSection Preliminaries / Entity and numeric character references\n");
        }

        // [Hexadecimal numeric character
        // references](@) consist of `&#` +
        // either `X` or `x` + a string of 1-6 hexadecimal digits + `;`.
        // They too are parsed as the corresponding Unicode character (this
        // time specified with a hexadecimal numeral instead of decimal).
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example027()
        {
            // Example 27
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &#X22; &#XD06; &#xcab;
            //
            // Should be rendered as:
            //     <p>&quot; ആ ಫ</p>

            TestParser.TestSpec("&#X22; &#XD06; &#xcab;", "<p>&quot; ആ ಫ</p>", "", context: "Example 27\nSection Preliminaries / Entity and numeric character references\n");
        }

        // Here are some nonentities:
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example028()
        {
            // Example 28
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &nbsp &x; &#; &#x;
            //     &#87654321;
            //     &#abcdef0;
            //     &ThisIsNotDefined; &hi?;
            //
            // Should be rendered as:
            //     <p>&amp;nbsp &amp;x; &amp;#; &amp;#x;
            //     &amp;#87654321;
            //     &amp;#abcdef0;
            //     &amp;ThisIsNotDefined; &amp;hi?;</p>

            TestParser.TestSpec("&nbsp &x; &#; &#x;\n&#87654321;\n&#abcdef0;\n&ThisIsNotDefined; &hi?;", "<p>&amp;nbsp &amp;x; &amp;#; &amp;#x;\n&amp;#87654321;\n&amp;#abcdef0;\n&amp;ThisIsNotDefined; &amp;hi?;</p>", "", context: "Example 28\nSection Preliminaries / Entity and numeric character references\n");
        }

        // Although HTML5 does accept some entity references
        // without a trailing semicolon (such as `&copy`), these are not
        // recognized here, because it makes the grammar too ambiguous:
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example029()
        {
            // Example 29
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &copy
            //
            // Should be rendered as:
            //     <p>&amp;copy</p>

            TestParser.TestSpec("&copy", "<p>&amp;copy</p>", "", context: "Example 29\nSection Preliminaries / Entity and numeric character references\n");
        }

        // Strings that are not on the list of HTML5 named entities are not
        // recognized as entity references either:
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example030()
        {
            // Example 30
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &MadeUpEntity;
            //
            // Should be rendered as:
            //     <p>&amp;MadeUpEntity;</p>

            TestParser.TestSpec("&MadeUpEntity;", "<p>&amp;MadeUpEntity;</p>", "", context: "Example 30\nSection Preliminaries / Entity and numeric character references\n");
        }

        // Entity and numeric character references are recognized in any
        // context besides code spans or code blocks, including
        // URLs, [link titles], and [fenced code block][] [info strings]:
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example031()
        {
            // Example 31
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     <a href="&ouml;&ouml;.html">
            //
            // Should be rendered as:
            //     <a href="&ouml;&ouml;.html">

            TestParser.TestSpec("<a href=\"&ouml;&ouml;.html\">", "<a href=\"&ouml;&ouml;.html\">", "", context: "Example 31\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example032()
        {
            // Example 32
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     [foo](/f&ouml;&ouml; "f&ouml;&ouml;")
            //
            // Should be rendered as:
            //     <p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>

            TestParser.TestSpec("[foo](/f&ouml;&ouml; \"f&ouml;&ouml;\")", "<p><a href=\"/f%C3%B6%C3%B6\" title=\"föö\">foo</a></p>", "", context: "Example 32\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example033()
        {
            // Example 33
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     [foo]
            //     
            //     [foo]: /f&ouml;&ouml; "f&ouml;&ouml;"
            //
            // Should be rendered as:
            //     <p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>

            TestParser.TestSpec("[foo]\n\n[foo]: /f&ouml;&ouml; \"f&ouml;&ouml;\"", "<p><a href=\"/f%C3%B6%C3%B6\" title=\"föö\">foo</a></p>", "", context: "Example 33\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example034()
        {
            // Example 34
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     ``` f&ouml;&ouml;
            //     foo
            //     ```
            //
            // Should be rendered as:
            //     <pre><code class="language-föö">foo
            //     </code></pre>

            TestParser.TestSpec("``` f&ouml;&ouml;\nfoo\n```", "<pre><code class=\"language-föö\">foo\n</code></pre>", "", context: "Example 34\nSection Preliminaries / Entity and numeric character references\n");
        }

        // Entity and numeric character references are treated as literal
        // text in code spans and code blocks:
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example035()
        {
            // Example 35
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     `f&ouml;&ouml;`
            //
            // Should be rendered as:
            //     <p><code>f&amp;ouml;&amp;ouml;</code></p>

            TestParser.TestSpec("`f&ouml;&ouml;`", "<p><code>f&amp;ouml;&amp;ouml;</code></p>", "", context: "Example 35\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example036()
        {
            // Example 36
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //         f&ouml;f&ouml;
            //
            // Should be rendered as:
            //     <pre><code>f&amp;ouml;f&amp;ouml;
            //     </code></pre>

            TestParser.TestSpec("    f&ouml;f&ouml;", "<pre><code>f&amp;ouml;f&amp;ouml;\n</code></pre>", "", context: "Example 36\nSection Preliminaries / Entity and numeric character references\n");
        }

        // Entity and numeric character references cannot be used
        // in place of symbols indicating structure in CommonMark
        // documents.
        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example037()
        {
            // Example 37
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &#42;foo&#42;
            //     *foo*
            //
            // Should be rendered as:
            //     <p>*foo*
            //     <em>foo</em></p>

            TestParser.TestSpec("&#42;foo&#42;\n*foo*", "<p>*foo*\n<em>foo</em></p>", "", context: "Example 37\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example038()
        {
            // Example 38
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &#42; foo
            //     
            //     * foo
            //
            // Should be rendered as:
            //     <p>* foo</p>
            //     <ul>
            //     <li>foo</li>
            //     </ul>

            TestParser.TestSpec("&#42; foo\n\n* foo", "<p>* foo</p>\n<ul>\n<li>foo</li>\n</ul>", "", context: "Example 38\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example039()
        {
            // Example 39
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     foo&#10;&#10;bar
            //
            // Should be rendered as:
            //     <p>foo
            //     
            //     bar</p>

            TestParser.TestSpec("foo&#10;&#10;bar", "<p>foo\n\nbar</p>", "", context: "Example 39\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example040()
        {
            // Example 40
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     &#9;foo
            //
            // Should be rendered as:
            //     <p>→foo</p>

            TestParser.TestSpec("&#9;foo", "<p>\tfoo</p>", "", context: "Example 40\nSection Preliminaries / Entity and numeric character references\n");
        }

        [Test]
        public void PreliminariesEntityAndNumericCharacterReferences_Example041()
        {
            // Example 41
            // Section: Preliminaries / Entity and numeric character references
            //
            // The following Markdown:
            //     [a](url &quot;tit&quot;)
            //
            // Should be rendered as:
            //     <p>[a](url &quot;tit&quot;)</p>

            TestParser.TestSpec("[a](url &quot;tit&quot;)", "<p>[a](url &quot;tit&quot;)</p>", "", context: "Example 41\nSection Preliminaries / Entity and numeric character references\n");
        }
    }

    [TestFixture]
    public class TestBlocksAndInlinesPrecedence
    {
        // # Blocks and inlines
        // 
        // We can think of a document as a sequence of
        // [blocks](@)---structural elements like paragraphs, block
        // quotations, lists, headings, rules, and code blocks.  Some blocks (like
        // block quotes and list items) contain other blocks; others (like
        // headings and paragraphs) contain [inline](@) content---text,
        // links, emphasized text, images, code spans, and so on.
        // 
        // ## Precedence
        // 
        // Indicators of block structure always take precedence over indicators
        // of inline structure.  So, for example, the following is a list with
        // two items, not a list with one item containing a code span:
        [Test]
        public void BlocksAndInlinesPrecedence_Example042()
        {
            // Example 42
            // Section: Blocks and inlines / Precedence
            //
            // The following Markdown:
            //     - `one
            //     - two`
            //
            // Should be rendered as:
            //     <ul>
            //     <li>`one</li>
            //     <li>two`</li>
            //     </ul>

            TestParser.TestSpec("- `one\n- two`", "<ul>\n<li>`one</li>\n<li>two`</li>\n</ul>", "", context: "Example 42\nSection Blocks and inlines / Precedence\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksThematicBreaks
    {
        // This means that parsing can proceed in two steps:  first, the block
        // structure of the document can be discerned; second, text lines inside
        // paragraphs, headings, and other block constructs can be parsed for inline
        // structure.  The second step requires information about link reference
        // definitions that will be available only at the end of the first
        // step.  Note that the first step requires processing lines in sequence,
        // but the second can be parallelized, since the inline parsing of
        // one block element does not affect the inline parsing of any other.
        // 
        // ## Container blocks and leaf blocks
        // 
        // We can divide blocks into two types:
        // [container blocks](#container-blocks),
        // which can contain other blocks, and [leaf blocks](#leaf-blocks),
        // which cannot.
        // 
        // # Leaf blocks
        // 
        // This section describes the different kinds of leaf block that make up a
        // Markdown document.
        // 
        // ## Thematic breaks
        // 
        // A line consisting of optionally up to three spaces of indentation, followed by a
        // sequence of three or more matching `-`, `_`, or `*` characters, each followed
        // optionally by any number of spaces or tabs, forms a
        // [thematic break](@).
        [Test]
        public void LeafBlocksThematicBreaks_Example043()
        {
            // Example 43
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     ***
            //     ---
            //     ___
            //
            // Should be rendered as:
            //     <hr />
            //     <hr />
            //     <hr />

            TestParser.TestSpec("***\n---\n___", "<hr />\n<hr />\n<hr />", "", context: "Example 43\nSection Leaf blocks / Thematic breaks\n");
        }

        // Wrong characters:
        [Test]
        public void LeafBlocksThematicBreaks_Example044()
        {
            // Example 44
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     +++
            //
            // Should be rendered as:
            //     <p>+++</p>

            TestParser.TestSpec("+++", "<p>+++</p>", "", context: "Example 44\nSection Leaf blocks / Thematic breaks\n");
        }

        [Test]
        public void LeafBlocksThematicBreaks_Example045()
        {
            // Example 45
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     ===
            //
            // Should be rendered as:
            //     <p>===</p>

            TestParser.TestSpec("===", "<p>===</p>", "", context: "Example 45\nSection Leaf blocks / Thematic breaks\n");
        }

        // Not enough characters:
        [Test]
        public void LeafBlocksThematicBreaks_Example046()
        {
            // Example 46
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     --
            //     **
            //     __
            //
            // Should be rendered as:
            //     <p>--
            //     **
            //     __</p>

            TestParser.TestSpec("--\n**\n__", "<p>--\n**\n__</p>", "", context: "Example 46\nSection Leaf blocks / Thematic breaks\n");
        }

        // Up to three spaces of indentation are allowed:
        [Test]
        public void LeafBlocksThematicBreaks_Example047()
        {
            // Example 47
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //      ***
            //       ***
            //        ***
            //
            // Should be rendered as:
            //     <hr />
            //     <hr />
            //     <hr />

            TestParser.TestSpec(" ***\n  ***\n   ***", "<hr />\n<hr />\n<hr />", "", context: "Example 47\nSection Leaf blocks / Thematic breaks\n");
        }

        // Four spaces of indentation is too many:
        [Test]
        public void LeafBlocksThematicBreaks_Example048()
        {
            // Example 48
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //         ***
            //
            // Should be rendered as:
            //     <pre><code>***
            //     </code></pre>

            TestParser.TestSpec("    ***", "<pre><code>***\n</code></pre>", "", context: "Example 48\nSection Leaf blocks / Thematic breaks\n");
        }

        [Test]
        public void LeafBlocksThematicBreaks_Example049()
        {
            // Example 49
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     Foo
            //         ***
            //
            // Should be rendered as:
            //     <p>Foo
            //     ***</p>

            TestParser.TestSpec("Foo\n    ***", "<p>Foo\n***</p>", "", context: "Example 49\nSection Leaf blocks / Thematic breaks\n");
        }

        // More than three characters may be used:
        [Test]
        public void LeafBlocksThematicBreaks_Example050()
        {
            // Example 50
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     _____________________________________
            //
            // Should be rendered as:
            //     <hr />

            TestParser.TestSpec("_____________________________________", "<hr />", "", context: "Example 50\nSection Leaf blocks / Thematic breaks\n");
        }

        // Spaces and tabs are allowed between the characters:
        [Test]
        public void LeafBlocksThematicBreaks_Example051()
        {
            // Example 51
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //      - - -
            //
            // Should be rendered as:
            //     <hr />

            TestParser.TestSpec(" - - -", "<hr />", "", context: "Example 51\nSection Leaf blocks / Thematic breaks\n");
        }

        [Test]
        public void LeafBlocksThematicBreaks_Example052()
        {
            // Example 52
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //      **  * ** * ** * **
            //
            // Should be rendered as:
            //     <hr />

            TestParser.TestSpec(" **  * ** * ** * **", "<hr />", "", context: "Example 52\nSection Leaf blocks / Thematic breaks\n");
        }

        [Test]
        public void LeafBlocksThematicBreaks_Example053()
        {
            // Example 53
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     -     -      -      -
            //
            // Should be rendered as:
            //     <hr />

            TestParser.TestSpec("-     -      -      -", "<hr />", "", context: "Example 53\nSection Leaf blocks / Thematic breaks\n");
        }

        // Spaces and tabs are allowed at the end:
        [Test]
        public void LeafBlocksThematicBreaks_Example054()
        {
            // Example 54
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     - - - -    
            //
            // Should be rendered as:
            //     <hr />

            TestParser.TestSpec("- - - -    ", "<hr />", "", context: "Example 54\nSection Leaf blocks / Thematic breaks\n");
        }

        // However, no other characters may occur in the line:
        [Test]
        public void LeafBlocksThematicBreaks_Example055()
        {
            // Example 55
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     _ _ _ _ a
            //     
            //     a------
            //     
            //     ---a---
            //
            // Should be rendered as:
            //     <p>_ _ _ _ a</p>
            //     <p>a------</p>
            //     <p>---a---</p>

            TestParser.TestSpec("_ _ _ _ a\n\na------\n\n---a---", "<p>_ _ _ _ a</p>\n<p>a------</p>\n<p>---a---</p>", "", context: "Example 55\nSection Leaf blocks / Thematic breaks\n");
        }

        // It is required that all of the characters other than spaces or tabs be the same.
        // So, this is not a thematic break:
        [Test]
        public void LeafBlocksThematicBreaks_Example056()
        {
            // Example 56
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //      *-*
            //
            // Should be rendered as:
            //     <p><em>-</em></p>

            TestParser.TestSpec(" *-*", "<p><em>-</em></p>", "", context: "Example 56\nSection Leaf blocks / Thematic breaks\n");
        }

        // Thematic breaks do not need blank lines before or after:
        [Test]
        public void LeafBlocksThematicBreaks_Example057()
        {
            // Example 57
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     - foo
            //     ***
            //     - bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     <hr />
            //     <ul>
            //     <li>bar</li>
            //     </ul>

            TestParser.TestSpec("- foo\n***\n- bar", "<ul>\n<li>foo</li>\n</ul>\n<hr />\n<ul>\n<li>bar</li>\n</ul>", "", context: "Example 57\nSection Leaf blocks / Thematic breaks\n");
        }

        // Thematic breaks can interrupt a paragraph:
        [Test]
        public void LeafBlocksThematicBreaks_Example058()
        {
            // Example 58
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     Foo
            //     ***
            //     bar
            //
            // Should be rendered as:
            //     <p>Foo</p>
            //     <hr />
            //     <p>bar</p>

            TestParser.TestSpec("Foo\n***\nbar", "<p>Foo</p>\n<hr />\n<p>bar</p>", "", context: "Example 58\nSection Leaf blocks / Thematic breaks\n");
        }

        // If a line of dashes that meets the above conditions for being a
        // thematic break could also be interpreted as the underline of a [setext
        // heading], the interpretation as a
        // [setext heading] takes precedence. Thus, for example,
        // this is a setext heading, not a paragraph followed by a thematic break:
        [Test]
        public void LeafBlocksThematicBreaks_Example059()
        {
            // Example 59
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     Foo
            //     ---
            //     bar
            //
            // Should be rendered as:
            //     <h2>Foo</h2>
            //     <p>bar</p>

            TestParser.TestSpec("Foo\n---\nbar", "<h2>Foo</h2>\n<p>bar</p>", "", context: "Example 59\nSection Leaf blocks / Thematic breaks\n");
        }

        // When both a thematic break and a list item are possible
        // interpretations of a line, the thematic break takes precedence:
        [Test]
        public void LeafBlocksThematicBreaks_Example060()
        {
            // Example 60
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     * Foo
            //     * * *
            //     * Bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>Foo</li>
            //     </ul>
            //     <hr />
            //     <ul>
            //     <li>Bar</li>
            //     </ul>

            TestParser.TestSpec("* Foo\n* * *\n* Bar", "<ul>\n<li>Foo</li>\n</ul>\n<hr />\n<ul>\n<li>Bar</li>\n</ul>", "", context: "Example 60\nSection Leaf blocks / Thematic breaks\n");
        }

        // If you want a thematic break in a list item, use a different bullet:
        [Test]
        public void LeafBlocksThematicBreaks_Example061()
        {
            // Example 61
            // Section: Leaf blocks / Thematic breaks
            //
            // The following Markdown:
            //     - Foo
            //     - * * *
            //
            // Should be rendered as:
            //     <ul>
            //     <li>Foo</li>
            //     <li>
            //     <hr />
            //     </li>
            //     </ul>

            TestParser.TestSpec("- Foo\n- * * *", "<ul>\n<li>Foo</li>\n<li>\n<hr />\n</li>\n</ul>", "", context: "Example 61\nSection Leaf blocks / Thematic breaks\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksATXHeadings
    {
        // ## ATX headings
        // 
        // An [ATX heading](@)
        // consists of a string of characters, parsed as inline content, between an
        // opening sequence of 1--6 unescaped `#` characters and an optional
        // closing sequence of any number of unescaped `#` characters.
        // The opening sequence of `#` characters must be followed by spaces or tabs, or
        // by the end of line. The optional closing sequence of `#`s must be preceded by
        // spaces or tabs and may be followed by spaces or tabs only.  The opening
        // `#` character may be preceded by up to three spaces of indentation.  The raw
        // contents of the heading are stripped of leading and trailing space or tabs
        // before being parsed as inline content.  The heading level is equal to the number
        // of `#` characters in the opening sequence.
        // 
        // Simple headings:
        [Test]
        public void LeafBlocksATXHeadings_Example062()
        {
            // Example 62
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     # foo
            //     ## foo
            //     ### foo
            //     #### foo
            //     ##### foo
            //     ###### foo
            //
            // Should be rendered as:
            //     <h1>foo</h1>
            //     <h2>foo</h2>
            //     <h3>foo</h3>
            //     <h4>foo</h4>
            //     <h5>foo</h5>
            //     <h6>foo</h6>

            TestParser.TestSpec("# foo\n## foo\n### foo\n#### foo\n##### foo\n###### foo", "<h1>foo</h1>\n<h2>foo</h2>\n<h3>foo</h3>\n<h4>foo</h4>\n<h5>foo</h5>\n<h6>foo</h6>", "", context: "Example 62\nSection Leaf blocks / ATX headings\n");
        }

        // More than six `#` characters is not a heading:
        [Test]
        public void LeafBlocksATXHeadings_Example063()
        {
            // Example 63
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     ####### foo
            //
            // Should be rendered as:
            //     <p>####### foo</p>

            TestParser.TestSpec("####### foo", "<p>####### foo</p>", "", context: "Example 63\nSection Leaf blocks / ATX headings\n");
        }

        // At least one space or tab is required between the `#` characters and the
        // heading's contents, unless the heading is empty.  Note that many
        // implementations currently do not require the space.  However, the
        // space was required by the
        // [original ATX implementation](http://www.aaronsw.com/2002/atx/atx.py),
        // and it helps prevent things like the following from being parsed as
        // headings:
        [Test]
        public void LeafBlocksATXHeadings_Example064()
        {
            // Example 64
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     #5 bolt
            //     
            //     #hashtag
            //
            // Should be rendered as:
            //     <p>#5 bolt</p>
            //     <p>#hashtag</p>

            TestParser.TestSpec("#5 bolt\n\n#hashtag", "<p>#5 bolt</p>\n<p>#hashtag</p>", "", context: "Example 64\nSection Leaf blocks / ATX headings\n");
        }

        // This is not a heading, because the first `#` is escaped:
        [Test]
        public void LeafBlocksATXHeadings_Example065()
        {
            // Example 65
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     \## foo
            //
            // Should be rendered as:
            //     <p>## foo</p>

            TestParser.TestSpec("\\## foo", "<p>## foo</p>", "", context: "Example 65\nSection Leaf blocks / ATX headings\n");
        }

        // Contents are parsed as inlines:
        [Test]
        public void LeafBlocksATXHeadings_Example066()
        {
            // Example 66
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     # foo *bar* \*baz\*
            //
            // Should be rendered as:
            //     <h1>foo <em>bar</em> *baz*</h1>

            TestParser.TestSpec("# foo *bar* \\*baz\\*", "<h1>foo <em>bar</em> *baz*</h1>", "", context: "Example 66\nSection Leaf blocks / ATX headings\n");
        }

        // Leading and trailing spaces or tabs are ignored in parsing inline content:
        [Test]
        public void LeafBlocksATXHeadings_Example067()
        {
            // Example 67
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     #                  foo                     
            //
            // Should be rendered as:
            //     <h1>foo</h1>

            TestParser.TestSpec("#                  foo                     ", "<h1>foo</h1>", "", context: "Example 67\nSection Leaf blocks / ATX headings\n");
        }

        // Up to three spaces of indentation are allowed:
        [Test]
        public void LeafBlocksATXHeadings_Example068()
        {
            // Example 68
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //      ### foo
            //       ## foo
            //        # foo
            //
            // Should be rendered as:
            //     <h3>foo</h3>
            //     <h2>foo</h2>
            //     <h1>foo</h1>

            TestParser.TestSpec(" ### foo\n  ## foo\n   # foo", "<h3>foo</h3>\n<h2>foo</h2>\n<h1>foo</h1>", "", context: "Example 68\nSection Leaf blocks / ATX headings\n");
        }

        // Four spaces of indentation is too many:
        [Test]
        public void LeafBlocksATXHeadings_Example069()
        {
            // Example 69
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //         # foo
            //
            // Should be rendered as:
            //     <pre><code># foo
            //     </code></pre>

            TestParser.TestSpec("    # foo", "<pre><code># foo\n</code></pre>", "", context: "Example 69\nSection Leaf blocks / ATX headings\n");
        }

        [Test]
        public void LeafBlocksATXHeadings_Example070()
        {
            // Example 70
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     foo
            //         # bar
            //
            // Should be rendered as:
            //     <p>foo
            //     # bar</p>

            TestParser.TestSpec("foo\n    # bar", "<p>foo\n# bar</p>", "", context: "Example 70\nSection Leaf blocks / ATX headings\n");
        }

        // A closing sequence of `#` characters is optional:
        [Test]
        public void LeafBlocksATXHeadings_Example071()
        {
            // Example 71
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     ## foo ##
            //       ###   bar    ###
            //
            // Should be rendered as:
            //     <h2>foo</h2>
            //     <h3>bar</h3>

            TestParser.TestSpec("## foo ##\n  ###   bar    ###", "<h2>foo</h2>\n<h3>bar</h3>", "", context: "Example 71\nSection Leaf blocks / ATX headings\n");
        }

        // It need not be the same length as the opening sequence:
        [Test]
        public void LeafBlocksATXHeadings_Example072()
        {
            // Example 72
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     # foo ##################################
            //     ##### foo ##
            //
            // Should be rendered as:
            //     <h1>foo</h1>
            //     <h5>foo</h5>

            TestParser.TestSpec("# foo ##################################\n##### foo ##", "<h1>foo</h1>\n<h5>foo</h5>", "", context: "Example 72\nSection Leaf blocks / ATX headings\n");
        }

        // Spaces or tabs are allowed after the closing sequence:
        [Test]
        public void LeafBlocksATXHeadings_Example073()
        {
            // Example 73
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     ### foo ###     
            //
            // Should be rendered as:
            //     <h3>foo</h3>

            TestParser.TestSpec("### foo ###     ", "<h3>foo</h3>", "", context: "Example 73\nSection Leaf blocks / ATX headings\n");
        }

        // A sequence of `#` characters with anything but spaces or tabs following it
        // is not a closing sequence, but counts as part of the contents of the
        // heading:
        [Test]
        public void LeafBlocksATXHeadings_Example074()
        {
            // Example 74
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     ### foo ### b
            //
            // Should be rendered as:
            //     <h3>foo ### b</h3>

            TestParser.TestSpec("### foo ### b", "<h3>foo ### b</h3>", "", context: "Example 74\nSection Leaf blocks / ATX headings\n");
        }

        // The closing sequence must be preceded by a space or tab:
        [Test]
        public void LeafBlocksATXHeadings_Example075()
        {
            // Example 75
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     # foo#
            //
            // Should be rendered as:
            //     <h1>foo#</h1>

            TestParser.TestSpec("# foo#", "<h1>foo#</h1>", "", context: "Example 75\nSection Leaf blocks / ATX headings\n");
        }

        // Backslash-escaped `#` characters do not count as part
        // of the closing sequence:
        [Test]
        public void LeafBlocksATXHeadings_Example076()
        {
            // Example 76
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     ### foo \###
            //     ## foo #\##
            //     # foo \#
            //
            // Should be rendered as:
            //     <h3>foo ###</h3>
            //     <h2>foo ###</h2>
            //     <h1>foo #</h1>

            TestParser.TestSpec("### foo \\###\n## foo #\\##\n# foo \\#", "<h3>foo ###</h3>\n<h2>foo ###</h2>\n<h1>foo #</h1>", "", context: "Example 76\nSection Leaf blocks / ATX headings\n");
        }

        // ATX headings need not be separated from surrounding content by blank
        // lines, and they can interrupt paragraphs:
        [Test]
        public void LeafBlocksATXHeadings_Example077()
        {
            // Example 77
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     ****
            //     ## foo
            //     ****
            //
            // Should be rendered as:
            //     <hr />
            //     <h2>foo</h2>
            //     <hr />

            TestParser.TestSpec("****\n## foo\n****", "<hr />\n<h2>foo</h2>\n<hr />", "", context: "Example 77\nSection Leaf blocks / ATX headings\n");
        }

        [Test]
        public void LeafBlocksATXHeadings_Example078()
        {
            // Example 78
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     Foo bar
            //     # baz
            //     Bar foo
            //
            // Should be rendered as:
            //     <p>Foo bar</p>
            //     <h1>baz</h1>
            //     <p>Bar foo</p>

            TestParser.TestSpec("Foo bar\n# baz\nBar foo", "<p>Foo bar</p>\n<h1>baz</h1>\n<p>Bar foo</p>", "", context: "Example 78\nSection Leaf blocks / ATX headings\n");
        }

        // ATX headings can be empty:
        [Test]
        public void LeafBlocksATXHeadings_Example079()
        {
            // Example 79
            // Section: Leaf blocks / ATX headings
            //
            // The following Markdown:
            //     ## 
            //     #
            //     ### ###
            //
            // Should be rendered as:
            //     <h2></h2>
            //     <h1></h1>
            //     <h3></h3>

            TestParser.TestSpec("## \n#\n### ###", "<h2></h2>\n<h1></h1>\n<h3></h3>", "", context: "Example 79\nSection Leaf blocks / ATX headings\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksSetextHeadings
    {
        // ## Setext headings
        // 
        // A [setext heading](@) consists of one or more
        // lines of text, not interrupted by a blank line, of which the first line does not
        // have more than 3 spaces of indentation, followed by
        // a [setext heading underline].  The lines of text must be such
        // that, were they not followed by the setext heading underline,
        // they would be interpreted as a paragraph:  they cannot be
        // interpretable as a [code fence], [ATX heading][ATX headings],
        // [block quote][block quotes], [thematic break][thematic breaks],
        // [list item][list items], or [HTML block][HTML blocks].
        // 
        // A [setext heading underline](@) is a sequence of
        // `=` characters or a sequence of `-` characters, with no more than 3
        // spaces of indentation and any number of trailing spaces or tabs.
        // 
        // The heading is a level 1 heading if `=` characters are used in
        // the [setext heading underline], and a level 2 heading if `-`
        // characters are used.  The contents of the heading are the result
        // of parsing the preceding lines of text as CommonMark inline
        // content.
        // 
        // In general, a setext heading need not be preceded or followed by a
        // blank line.  However, it cannot interrupt a paragraph, so when a
        // setext heading comes after a paragraph, a blank line is needed between
        // them.
        // 
        // Simple examples:
        [Test]
        public void LeafBlocksSetextHeadings_Example080()
        {
            // Example 80
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo *bar*
            //     =========
            //     
            //     Foo *bar*
            //     ---------
            //
            // Should be rendered as:
            //     <h1>Foo <em>bar</em></h1>
            //     <h2>Foo <em>bar</em></h2>

            TestParser.TestSpec("Foo *bar*\n=========\n\nFoo *bar*\n---------", "<h1>Foo <em>bar</em></h1>\n<h2>Foo <em>bar</em></h2>", "", context: "Example 80\nSection Leaf blocks / Setext headings\n");
        }

        // The content of the header may span more than one line:
        [Test]
        public void LeafBlocksSetextHeadings_Example081()
        {
            // Example 81
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo *bar
            //     baz*
            //     ====
            //
            // Should be rendered as:
            //     <h1>Foo <em>bar
            //     baz</em></h1>

            TestParser.TestSpec("Foo *bar\nbaz*\n====", "<h1>Foo <em>bar\nbaz</em></h1>", "", context: "Example 81\nSection Leaf blocks / Setext headings\n");
        }

        // The contents are the result of parsing the headings's raw
        // content as inlines.  The heading's raw content is formed by
        // concatenating the lines and removing initial and final
        // spaces or tabs.
        [Test]
        public void LeafBlocksSetextHeadings_Example082()
        {
            // Example 82
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //       Foo *bar
            //     baz*→
            //     ====
            //
            // Should be rendered as:
            //     <h1>Foo <em>bar
            //     baz</em></h1>

            TestParser.TestSpec("  Foo *bar\nbaz*\t\n====", "<h1>Foo <em>bar\nbaz</em></h1>", "", context: "Example 82\nSection Leaf blocks / Setext headings\n");
        }

        // The underlining can be any length:
        [Test]
        public void LeafBlocksSetextHeadings_Example083()
        {
            // Example 83
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //     -------------------------
            //     
            //     Foo
            //     =
            //
            // Should be rendered as:
            //     <h2>Foo</h2>
            //     <h1>Foo</h1>

            TestParser.TestSpec("Foo\n-------------------------\n\nFoo\n=", "<h2>Foo</h2>\n<h1>Foo</h1>", "", context: "Example 83\nSection Leaf blocks / Setext headings\n");
        }

        // The heading content can be preceded by up to three spaces of indentation, and
        // need not line up with the underlining:
        [Test]
        public void LeafBlocksSetextHeadings_Example084()
        {
            // Example 84
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //        Foo
            //     ---
            //     
            //       Foo
            //     -----
            //     
            //       Foo
            //       ===
            //
            // Should be rendered as:
            //     <h2>Foo</h2>
            //     <h2>Foo</h2>
            //     <h1>Foo</h1>

            TestParser.TestSpec("   Foo\n---\n\n  Foo\n-----\n\n  Foo\n  ===", "<h2>Foo</h2>\n<h2>Foo</h2>\n<h1>Foo</h1>", "", context: "Example 84\nSection Leaf blocks / Setext headings\n");
        }

        // Four spaces of indentation is too many:
        [Test]
        public void LeafBlocksSetextHeadings_Example085()
        {
            // Example 85
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //         Foo
            //         ---
            //     
            //         Foo
            //     ---
            //
            // Should be rendered as:
            //     <pre><code>Foo
            //     ---
            //     
            //     Foo
            //     </code></pre>
            //     <hr />

            TestParser.TestSpec("    Foo\n    ---\n\n    Foo\n---", "<pre><code>Foo\n---\n\nFoo\n</code></pre>\n<hr />", "", context: "Example 85\nSection Leaf blocks / Setext headings\n");
        }

        // The setext heading underline can be preceded by up to three spaces of
        // indentation, and may have trailing spaces or tabs:
        [Test]
        public void LeafBlocksSetextHeadings_Example086()
        {
            // Example 86
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //        ----      
            //
            // Should be rendered as:
            //     <h2>Foo</h2>

            TestParser.TestSpec("Foo\n   ----      ", "<h2>Foo</h2>", "", context: "Example 86\nSection Leaf blocks / Setext headings\n");
        }

        // Four spaces of indentation is too many:
        [Test]
        public void LeafBlocksSetextHeadings_Example087()
        {
            // Example 87
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //         ---
            //
            // Should be rendered as:
            //     <p>Foo
            //     ---</p>

            TestParser.TestSpec("Foo\n    ---", "<p>Foo\n---</p>", "", context: "Example 87\nSection Leaf blocks / Setext headings\n");
        }

        // The setext heading underline cannot contain internal spaces or tabs:
        [Test]
        public void LeafBlocksSetextHeadings_Example088()
        {
            // Example 88
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //     = =
            //     
            //     Foo
            //     --- -
            //
            // Should be rendered as:
            //     <p>Foo
            //     = =</p>
            //     <p>Foo</p>
            //     <hr />

            TestParser.TestSpec("Foo\n= =\n\nFoo\n--- -", "<p>Foo\n= =</p>\n<p>Foo</p>\n<hr />", "", context: "Example 88\nSection Leaf blocks / Setext headings\n");
        }

        // Trailing spaces or tabs in the content line do not cause a hard line break:
        [Test]
        public void LeafBlocksSetextHeadings_Example089()
        {
            // Example 89
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo  
            //     -----
            //
            // Should be rendered as:
            //     <h2>Foo</h2>

            TestParser.TestSpec("Foo  \n-----", "<h2>Foo</h2>", "", context: "Example 89\nSection Leaf blocks / Setext headings\n");
        }

        // Nor does a backslash at the end:
        [Test]
        public void LeafBlocksSetextHeadings_Example090()
        {
            // Example 90
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo\
            //     ----
            //
            // Should be rendered as:
            //     <h2>Foo\</h2>

            TestParser.TestSpec("Foo\\\n----", "<h2>Foo\\</h2>", "", context: "Example 90\nSection Leaf blocks / Setext headings\n");
        }

        // Since indicators of block structure take precedence over
        // indicators of inline structure, the following are setext headings:
        [Test]
        public void LeafBlocksSetextHeadings_Example091()
        {
            // Example 91
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     `Foo
            //     ----
            //     `
            //     
            //     <a title="a lot
            //     ---
            //     of dashes"/>
            //
            // Should be rendered as:
            //     <h2>`Foo</h2>
            //     <p>`</p>
            //     <h2>&lt;a title=&quot;a lot</h2>
            //     <p>of dashes&quot;/&gt;</p>

            TestParser.TestSpec("`Foo\n----\n`\n\n<a title=\"a lot\n---\nof dashes\"/>", "<h2>`Foo</h2>\n<p>`</p>\n<h2>&lt;a title=&quot;a lot</h2>\n<p>of dashes&quot;/&gt;</p>", "", context: "Example 91\nSection Leaf blocks / Setext headings\n");
        }

        // The setext heading underline cannot be a [lazy continuation
        // line] in a list item or block quote:
        [Test]
        public void LeafBlocksSetextHeadings_Example092()
        {
            // Example 92
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     > Foo
            //     ---
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>Foo</p>
            //     </blockquote>
            //     <hr />

            TestParser.TestSpec("> Foo\n---", "<blockquote>\n<p>Foo</p>\n</blockquote>\n<hr />", "", context: "Example 92\nSection Leaf blocks / Setext headings\n");
        }

        [Test]
        public void LeafBlocksSetextHeadings_Example093()
        {
            // Example 93
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     > foo
            //     bar
            //     ===
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo
            //     bar
            //     ===</p>
            //     </blockquote>

            TestParser.TestSpec("> foo\nbar\n===", "<blockquote>\n<p>foo\nbar\n===</p>\n</blockquote>", "", context: "Example 93\nSection Leaf blocks / Setext headings\n");
        }

        [Test]
        public void LeafBlocksSetextHeadings_Example094()
        {
            // Example 94
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     - Foo
            //     ---
            //
            // Should be rendered as:
            //     <ul>
            //     <li>Foo</li>
            //     </ul>
            //     <hr />

            TestParser.TestSpec("- Foo\n---", "<ul>\n<li>Foo</li>\n</ul>\n<hr />", "", context: "Example 94\nSection Leaf blocks / Setext headings\n");
        }

        // A blank line is needed between a paragraph and a following
        // setext heading, since otherwise the paragraph becomes part
        // of the heading's content:
        [Test]
        public void LeafBlocksSetextHeadings_Example095()
        {
            // Example 95
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //     Bar
            //     ---
            //
            // Should be rendered as:
            //     <h2>Foo
            //     Bar</h2>

            TestParser.TestSpec("Foo\nBar\n---", "<h2>Foo\nBar</h2>", "", context: "Example 95\nSection Leaf blocks / Setext headings\n");
        }

        // But in general a blank line is not required before or after
        // setext headings:
        [Test]
        public void LeafBlocksSetextHeadings_Example096()
        {
            // Example 96
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     ---
            //     Foo
            //     ---
            //     Bar
            //     ---
            //     Baz
            //
            // Should be rendered as:
            //     <hr />
            //     <h2>Foo</h2>
            //     <h2>Bar</h2>
            //     <p>Baz</p>

            TestParser.TestSpec("---\nFoo\n---\nBar\n---\nBaz", "<hr />\n<h2>Foo</h2>\n<h2>Bar</h2>\n<p>Baz</p>", "", context: "Example 96\nSection Leaf blocks / Setext headings\n");
        }

        // Setext headings cannot be empty:
        [Test]
        public void LeafBlocksSetextHeadings_Example097()
        {
            // Example 97
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     
            //     ====
            //
            // Should be rendered as:
            //     <p>====</p>

            TestParser.TestSpec("\n====", "<p>====</p>", "", context: "Example 97\nSection Leaf blocks / Setext headings\n");
        }

        // Setext heading text lines must not be interpretable as block
        // constructs other than paragraphs.  So, the line of dashes
        // in these examples gets interpreted as a thematic break:
        [Test]
        public void LeafBlocksSetextHeadings_Example098()
        {
            // Example 98
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     ---
            //     ---
            //
            // Should be rendered as:
            //     <hr />
            //     <hr />

            TestParser.TestSpec("---\n---", "<hr />\n<hr />", "", context: "Example 98\nSection Leaf blocks / Setext headings\n");
        }

        [Test]
        public void LeafBlocksSetextHeadings_Example099()
        {
            // Example 99
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     - foo
            //     -----
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     <hr />

            TestParser.TestSpec("- foo\n-----", "<ul>\n<li>foo</li>\n</ul>\n<hr />", "", context: "Example 99\nSection Leaf blocks / Setext headings\n");
        }

        [Test]
        public void LeafBlocksSetextHeadings_Example100()
        {
            // Example 100
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //         foo
            //     ---
            //
            // Should be rendered as:
            //     <pre><code>foo
            //     </code></pre>
            //     <hr />

            TestParser.TestSpec("    foo\n---", "<pre><code>foo\n</code></pre>\n<hr />", "", context: "Example 100\nSection Leaf blocks / Setext headings\n");
        }

        [Test]
        public void LeafBlocksSetextHeadings_Example101()
        {
            // Example 101
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     > foo
            //     -----
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>
            //     <hr />

            TestParser.TestSpec("> foo\n-----", "<blockquote>\n<p>foo</p>\n</blockquote>\n<hr />", "", context: "Example 101\nSection Leaf blocks / Setext headings\n");
        }

        // If you want a heading with `> foo` as its literal text, you can
        // use backslash escapes:
        [Test]
        public void LeafBlocksSetextHeadings_Example102()
        {
            // Example 102
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     \> foo
            //     ------
            //
            // Should be rendered as:
            //     <h2>&gt; foo</h2>

            TestParser.TestSpec("\\> foo\n------", "<h2>&gt; foo</h2>", "", context: "Example 102\nSection Leaf blocks / Setext headings\n");
        }

        // **Compatibility note:**  Most existing Markdown implementations
        // do not allow the text of setext headings to span multiple lines.
        // But there is no consensus about how to interpret
        // 
        // ``` markdown
        // Foo
        // bar
        // ---
        // baz
        // ```
        // 
        // One can find four different interpretations:
        // 
        // 1. paragraph "Foo", heading "bar", paragraph "baz"
        // 2. paragraph "Foo bar", thematic break, paragraph "baz"
        // 3. paragraph "Foo bar --- baz"
        // 4. heading "Foo bar", paragraph "baz"
        // 
        // We find interpretation 4 most natural, and interpretation 4
        // increases the expressive power of CommonMark, by allowing
        // multiline headings.  Authors who want interpretation 1 can
        // put a blank line after the first paragraph:
        [Test]
        public void LeafBlocksSetextHeadings_Example103()
        {
            // Example 103
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //     
            //     bar
            //     ---
            //     baz
            //
            // Should be rendered as:
            //     <p>Foo</p>
            //     <h2>bar</h2>
            //     <p>baz</p>

            TestParser.TestSpec("Foo\n\nbar\n---\nbaz", "<p>Foo</p>\n<h2>bar</h2>\n<p>baz</p>", "", context: "Example 103\nSection Leaf blocks / Setext headings\n");
        }

        // Authors who want interpretation 2 can put blank lines around
        // the thematic break,
        [Test]
        public void LeafBlocksSetextHeadings_Example104()
        {
            // Example 104
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //     bar
            //     
            //     ---
            //     
            //     baz
            //
            // Should be rendered as:
            //     <p>Foo
            //     bar</p>
            //     <hr />
            //     <p>baz</p>

            TestParser.TestSpec("Foo\nbar\n\n---\n\nbaz", "<p>Foo\nbar</p>\n<hr />\n<p>baz</p>", "", context: "Example 104\nSection Leaf blocks / Setext headings\n");
        }

        // or use a thematic break that cannot count as a [setext heading
        // underline], such as
        [Test]
        public void LeafBlocksSetextHeadings_Example105()
        {
            // Example 105
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //     bar
            //     * * *
            //     baz
            //
            // Should be rendered as:
            //     <p>Foo
            //     bar</p>
            //     <hr />
            //     <p>baz</p>

            TestParser.TestSpec("Foo\nbar\n* * *\nbaz", "<p>Foo\nbar</p>\n<hr />\n<p>baz</p>", "", context: "Example 105\nSection Leaf blocks / Setext headings\n");
        }

        // Authors who want interpretation 3 can use backslash escapes:
        [Test]
        public void LeafBlocksSetextHeadings_Example106()
        {
            // Example 106
            // Section: Leaf blocks / Setext headings
            //
            // The following Markdown:
            //     Foo
            //     bar
            //     \---
            //     baz
            //
            // Should be rendered as:
            //     <p>Foo
            //     bar
            //     ---
            //     baz</p>

            TestParser.TestSpec("Foo\nbar\n\\---\nbaz", "<p>Foo\nbar\n---\nbaz</p>", "", context: "Example 106\nSection Leaf blocks / Setext headings\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksIndentedCodeBlocks
    {
        // ## Indented code blocks
        // 
        // An [indented code block](@) is composed of one or more
        // [indented chunks] separated by blank lines.
        // An [indented chunk](@) is a sequence of non-blank lines,
        // each preceded by four or more spaces of indentation. The contents of the code
        // block are the literal contents of the lines, including trailing
        // [line endings], minus four spaces of indentation.
        // An indented code block has no [info string].
        // 
        // An indented code block cannot interrupt a paragraph, so there must be
        // a blank line between a paragraph and a following indented code block.
        // (A blank line is not needed, however, between a code block and a following
        // paragraph.)
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example107()
        {
            // Example 107
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //         a simple
            //           indented code block
            //
            // Should be rendered as:
            //     <pre><code>a simple
            //       indented code block
            //     </code></pre>

            TestParser.TestSpec("    a simple\n      indented code block", "<pre><code>a simple\n  indented code block\n</code></pre>", "", context: "Example 107\nSection Leaf blocks / Indented code blocks\n");
        }

        // If there is any ambiguity between an interpretation of indentation
        // as a code block and as indicating that material belongs to a [list
        // item][list items], the list item interpretation takes precedence:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example108()
        {
            // Example 108
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //       - foo
            //     
            //         bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("  - foo\n\n    bar", "<ul>\n<li>\n<p>foo</p>\n<p>bar</p>\n</li>\n</ul>", "", context: "Example 108\nSection Leaf blocks / Indented code blocks\n");
        }

        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example109()
        {
            // Example 109
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //     1.  foo
            //     
            //         - bar
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>foo</p>
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1.  foo\n\n    - bar", "<ol>\n<li>\n<p>foo</p>\n<ul>\n<li>bar</li>\n</ul>\n</li>\n</ol>", "", context: "Example 109\nSection Leaf blocks / Indented code blocks\n");
        }

        // The contents of a code block are literal text, and do not get parsed
        // as Markdown:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example110()
        {
            // Example 110
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //         <a/>
            //         *hi*
            //     
            //         - one
            //
            // Should be rendered as:
            //     <pre><code>&lt;a/&gt;
            //     *hi*
            //     
            //     - one
            //     </code></pre>

            TestParser.TestSpec("    <a/>\n    *hi*\n\n    - one", "<pre><code>&lt;a/&gt;\n*hi*\n\n- one\n</code></pre>", "", context: "Example 110\nSection Leaf blocks / Indented code blocks\n");
        }

        // Here we have three chunks separated by blank lines:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example111()
        {
            // Example 111
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //         chunk1
            //     
            //         chunk2
            //       
            //      
            //      
            //         chunk3
            //
            // Should be rendered as:
            //     <pre><code>chunk1
            //     
            //     chunk2
            //     
            //     
            //     
            //     chunk3
            //     </code></pre>

            TestParser.TestSpec("    chunk1\n\n    chunk2\n  \n \n \n    chunk3", "<pre><code>chunk1\n\nchunk2\n\n\n\nchunk3\n</code></pre>", "", context: "Example 111\nSection Leaf blocks / Indented code blocks\n");
        }

        // Any initial spaces or tabs beyond four spaces of indentation will be included in
        // the content, even in interior blank lines:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example112()
        {
            // Example 112
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //         chunk1
            //           
            //           chunk2
            //
            // Should be rendered as:
            //     <pre><code>chunk1
            //       
            //       chunk2
            //     </code></pre>

            TestParser.TestSpec("    chunk1\n      \n      chunk2", "<pre><code>chunk1\n  \n  chunk2\n</code></pre>", "", context: "Example 112\nSection Leaf blocks / Indented code blocks\n");
        }

        // An indented code block cannot interrupt a paragraph.  (This
        // allows hanging indents and the like.)
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example113()
        {
            // Example 113
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //     Foo
            //         bar
            //     
            //
            // Should be rendered as:
            //     <p>Foo
            //     bar</p>

            TestParser.TestSpec("Foo\n    bar\n", "<p>Foo\nbar</p>", "", context: "Example 113\nSection Leaf blocks / Indented code blocks\n");
        }

        // However, any non-blank line with fewer than four spaces of indentation ends
        // the code block immediately.  So a paragraph may occur immediately
        // after indented code:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example114()
        {
            // Example 114
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //         foo
            //     bar
            //
            // Should be rendered as:
            //     <pre><code>foo
            //     </code></pre>
            //     <p>bar</p>

            TestParser.TestSpec("    foo\nbar", "<pre><code>foo\n</code></pre>\n<p>bar</p>", "", context: "Example 114\nSection Leaf blocks / Indented code blocks\n");
        }

        // And indented code can occur immediately before and after other kinds of
        // blocks:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example115()
        {
            // Example 115
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //     # Heading
            //         foo
            //     Heading
            //     ------
            //         foo
            //     ----
            //
            // Should be rendered as:
            //     <h1>Heading</h1>
            //     <pre><code>foo
            //     </code></pre>
            //     <h2>Heading</h2>
            //     <pre><code>foo
            //     </code></pre>
            //     <hr />

            TestParser.TestSpec("# Heading\n    foo\nHeading\n------\n    foo\n----", "<h1>Heading</h1>\n<pre><code>foo\n</code></pre>\n<h2>Heading</h2>\n<pre><code>foo\n</code></pre>\n<hr />", "", context: "Example 115\nSection Leaf blocks / Indented code blocks\n");
        }

        // The first line can be preceded by more than four spaces of indentation:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example116()
        {
            // Example 116
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //             foo
            //         bar
            //
            // Should be rendered as:
            //     <pre><code>    foo
            //     bar
            //     </code></pre>

            TestParser.TestSpec("        foo\n    bar", "<pre><code>    foo\nbar\n</code></pre>", "", context: "Example 116\nSection Leaf blocks / Indented code blocks\n");
        }

        // Blank lines preceding or following an indented code block
        // are not included in it:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example117()
        {
            // Example 117
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //     
            //         
            //         foo
            //         
            //     
            //
            // Should be rendered as:
            //     <pre><code>foo
            //     </code></pre>

            TestParser.TestSpec("\n    \n    foo\n    \n", "<pre><code>foo\n</code></pre>", "", context: "Example 117\nSection Leaf blocks / Indented code blocks\n");
        }

        // Trailing spaces or tabs are included in the code block's content:
        [Test]
        public void LeafBlocksIndentedCodeBlocks_Example118()
        {
            // Example 118
            // Section: Leaf blocks / Indented code blocks
            //
            // The following Markdown:
            //         foo  
            //
            // Should be rendered as:
            //     <pre><code>foo  
            //     </code></pre>

            TestParser.TestSpec("    foo  ", "<pre><code>foo  \n</code></pre>", "", context: "Example 118\nSection Leaf blocks / Indented code blocks\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksFencedCodeBlocks
    {
        // ## Fenced code blocks
        // 
        // A [code fence](@) is a sequence
        // of at least three consecutive backtick characters (`` ` ``) or
        // tildes (`~`).  (Tildes and backticks cannot be mixed.)
        // A [fenced code block](@)
        // begins with a code fence, preceded by up to three spaces of indentation.
        // 
        // The line with the opening code fence may optionally contain some text
        // following the code fence; this is trimmed of leading and trailing
        // spaces or tabs and called the [info string](@). If the [info string] comes
        // after a backtick fence, it may not contain any backtick
        // characters.  (The reason for this restriction is that otherwise
        // some inline code would be incorrectly interpreted as the
        // beginning of a fenced code block.)
        // 
        // The content of the code block consists of all subsequent lines, until
        // a closing [code fence] of the same type as the code block
        // began with (backticks or tildes), and with at least as many backticks
        // or tildes as the opening code fence.  If the leading code fence is
        // preceded by N spaces of indentation, then up to N spaces of indentation are
        // removed from each line of the content (if present).  (If a content line is not
        // indented, it is preserved unchanged.  If it is indented N spaces or less, all
        // of the indentation is removed.)
        // 
        // The closing code fence may be preceded by up to three spaces of indentation, and
        // may be followed only by spaces or tabs, which are ignored.  If the end of the
        // containing block (or document) is reached and no closing code fence
        // has been found, the code block contains all of the lines after the
        // opening code fence until the end of the containing block (or
        // document).  (An alternative spec would require backtracking in the
        // event that a closing code fence is not found.  But this makes parsing
        // much less efficient, and there seems to be no real downside to the
        // behavior described here.)
        // 
        // A fenced code block may interrupt a paragraph, and does not require
        // a blank line either before or after.
        // 
        // The content of a code fence is treated as literal text, not parsed
        // as inlines.  The first word of the [info string] is typically used to
        // specify the language of the code sample, and rendered in the `class`
        // attribute of the `code` tag.  However, this spec does not mandate any
        // particular treatment of the [info string].
        // 
        // Here is a simple example with backticks:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example119()
        {
            // Example 119
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //     <
            //      >
            //     ```
            //
            // Should be rendered as:
            //     <pre><code>&lt;
            //      &gt;
            //     </code></pre>

            TestParser.TestSpec("```\n<\n >\n```", "<pre><code>&lt;\n &gt;\n</code></pre>", "", context: "Example 119\nSection Leaf blocks / Fenced code blocks\n");
        }

        // With tildes:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example120()
        {
            // Example 120
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ~~~
            //     <
            //      >
            //     ~~~
            //
            // Should be rendered as:
            //     <pre><code>&lt;
            //      &gt;
            //     </code></pre>

            TestParser.TestSpec("~~~\n<\n >\n~~~", "<pre><code>&lt;\n &gt;\n</code></pre>", "", context: "Example 120\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Fewer than three backticks is not enough:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example121()
        {
            // Example 121
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ``
            //     foo
            //     ``
            //
            // Should be rendered as:
            //     <p><code>foo</code></p>

            TestParser.TestSpec("``\nfoo\n``", "<p><code>foo</code></p>", "", context: "Example 121\nSection Leaf blocks / Fenced code blocks\n");
        }

        // The closing code fence must use the same character as the opening
        // fence:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example122()
        {
            // Example 122
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //     aaa
            //     ~~~
            //     ```
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     ~~~
            //     </code></pre>

            TestParser.TestSpec("```\naaa\n~~~\n```", "<pre><code>aaa\n~~~\n</code></pre>", "", context: "Example 122\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example123()
        {
            // Example 123
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ~~~
            //     aaa
            //     ```
            //     ~~~
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     ```
            //     </code></pre>

            TestParser.TestSpec("~~~\naaa\n```\n~~~", "<pre><code>aaa\n```\n</code></pre>", "", context: "Example 123\nSection Leaf blocks / Fenced code blocks\n");
        }

        // The closing code fence must be at least as long as the opening fence:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example124()
        {
            // Example 124
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ````
            //     aaa
            //     ```
            //     ``````
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     ```
            //     </code></pre>

            TestParser.TestSpec("````\naaa\n```\n``````", "<pre><code>aaa\n```\n</code></pre>", "", context: "Example 124\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example125()
        {
            // Example 125
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ~~~~
            //     aaa
            //     ~~~
            //     ~~~~
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     ~~~
            //     </code></pre>

            TestParser.TestSpec("~~~~\naaa\n~~~\n~~~~", "<pre><code>aaa\n~~~\n</code></pre>", "", context: "Example 125\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Unclosed code blocks are closed by the end of the document
        // (or the enclosing [block quote][block quotes] or [list item][list items]):
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example126()
        {
            // Example 126
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //
            // Should be rendered as:
            //     <pre><code></code></pre>

            TestParser.TestSpec("```", "<pre><code></code></pre>", "", context: "Example 126\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example127()
        {
            // Example 127
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     `````
            //     
            //     ```
            //     aaa
            //
            // Should be rendered as:
            //     <pre><code>
            //     ```
            //     aaa
            //     </code></pre>

            TestParser.TestSpec("`````\n\n```\naaa", "<pre><code>\n```\naaa\n</code></pre>", "", context: "Example 127\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example128()
        {
            // Example 128
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     > ```
            //     > aaa
            //     
            //     bbb
            //
            // Should be rendered as:
            //     <blockquote>
            //     <pre><code>aaa
            //     </code></pre>
            //     </blockquote>
            //     <p>bbb</p>

            TestParser.TestSpec("> ```\n> aaa\n\nbbb", "<blockquote>\n<pre><code>aaa\n</code></pre>\n</blockquote>\n<p>bbb</p>", "", context: "Example 128\nSection Leaf blocks / Fenced code blocks\n");
        }

        // A code block can have all empty lines as its content:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example129()
        {
            // Example 129
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //     
            //       
            //     ```
            //
            // Should be rendered as:
            //     <pre><code>
            //       
            //     </code></pre>

            TestParser.TestSpec("```\n\n  \n```", "<pre><code>\n  \n</code></pre>", "", context: "Example 129\nSection Leaf blocks / Fenced code blocks\n");
        }

        // A code block can be empty:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example130()
        {
            // Example 130
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //     ```
            //
            // Should be rendered as:
            //     <pre><code></code></pre>

            TestParser.TestSpec("```\n```", "<pre><code></code></pre>", "", context: "Example 130\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Fences can be indented.  If the opening fence is indented,
        // content lines will have equivalent opening indentation removed,
        // if present:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example131()
        {
            // Example 131
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //      ```
            //      aaa
            //     aaa
            //     ```
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     aaa
            //     </code></pre>

            TestParser.TestSpec(" ```\n aaa\naaa\n```", "<pre><code>aaa\naaa\n</code></pre>", "", context: "Example 131\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example132()
        {
            // Example 132
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //       ```
            //     aaa
            //       aaa
            //     aaa
            //       ```
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     aaa
            //     aaa
            //     </code></pre>

            TestParser.TestSpec("  ```\naaa\n  aaa\naaa\n  ```", "<pre><code>aaa\naaa\naaa\n</code></pre>", "", context: "Example 132\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example133()
        {
            // Example 133
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //        ```
            //        aaa
            //         aaa
            //       aaa
            //        ```
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //      aaa
            //     aaa
            //     </code></pre>

            TestParser.TestSpec("   ```\n   aaa\n    aaa\n  aaa\n   ```", "<pre><code>aaa\n aaa\naaa\n</code></pre>", "", context: "Example 133\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Four spaces of indentation is too many:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example134()
        {
            // Example 134
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //         ```
            //         aaa
            //         ```
            //
            // Should be rendered as:
            //     <pre><code>```
            //     aaa
            //     ```
            //     </code></pre>

            TestParser.TestSpec("    ```\n    aaa\n    ```", "<pre><code>```\naaa\n```\n</code></pre>", "", context: "Example 134\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Closing fences may be preceded by up to three spaces of indentation, and their
        // indentation need not match that of the opening fence:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example135()
        {
            // Example 135
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //     aaa
            //       ```
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     </code></pre>

            TestParser.TestSpec("```\naaa\n  ```", "<pre><code>aaa\n</code></pre>", "", context: "Example 135\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example136()
        {
            // Example 136
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //        ```
            //     aaa
            //       ```
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     </code></pre>

            TestParser.TestSpec("   ```\naaa\n  ```", "<pre><code>aaa\n</code></pre>", "", context: "Example 136\nSection Leaf blocks / Fenced code blocks\n");
        }

        // This is not a closing fence, because it is indented 4 spaces:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example137()
        {
            // Example 137
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //     aaa
            //         ```
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //         ```
            //     </code></pre>

            TestParser.TestSpec("```\naaa\n    ```", "<pre><code>aaa\n    ```\n</code></pre>", "", context: "Example 137\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Code fences (opening and closing) cannot contain internal spaces or tabs:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example138()
        {
            // Example 138
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ``` ```
            //     aaa
            //
            // Should be rendered as:
            //     <p><code> </code>
            //     aaa</p>

            TestParser.TestSpec("``` ```\naaa", "<p><code> </code>\naaa</p>", "", context: "Example 138\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example139()
        {
            // Example 139
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ~~~~~~
            //     aaa
            //     ~~~ ~~
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     ~~~ ~~
            //     </code></pre>

            TestParser.TestSpec("~~~~~~\naaa\n~~~ ~~", "<pre><code>aaa\n~~~ ~~\n</code></pre>", "", context: "Example 139\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Fenced code blocks can interrupt paragraphs, and can be followed
        // directly by paragraphs, without a blank line between:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example140()
        {
            // Example 140
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     foo
            //     ```
            //     bar
            //     ```
            //     baz
            //
            // Should be rendered as:
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     <p>baz</p>

            TestParser.TestSpec("foo\n```\nbar\n```\nbaz", "<p>foo</p>\n<pre><code>bar\n</code></pre>\n<p>baz</p>", "", context: "Example 140\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Other blocks can also occur before and after fenced code blocks
        // without an intervening blank line:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example141()
        {
            // Example 141
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     foo
            //     ---
            //     ~~~
            //     bar
            //     ~~~
            //     # baz
            //
            // Should be rendered as:
            //     <h2>foo</h2>
            //     <pre><code>bar
            //     </code></pre>
            //     <h1>baz</h1>

            TestParser.TestSpec("foo\n---\n~~~\nbar\n~~~\n# baz", "<h2>foo</h2>\n<pre><code>bar\n</code></pre>\n<h1>baz</h1>", "", context: "Example 141\nSection Leaf blocks / Fenced code blocks\n");
        }

        // An [info string] can be provided after the opening code fence.
        // Although this spec doesn't mandate any particular treatment of
        // the info string, the first word is typically used to specify
        // the language of the code block. In HTML output, the language is
        // normally indicated by adding a class to the `code` element consisting
        // of `language-` followed by the language name.
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example142()
        {
            // Example 142
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```ruby
            //     def foo(x)
            //       return 3
            //     end
            //     ```
            //
            // Should be rendered as:
            //     <pre><code class="language-ruby">def foo(x)
            //       return 3
            //     end
            //     </code></pre>

            TestParser.TestSpec("```ruby\ndef foo(x)\n  return 3\nend\n```", "<pre><code class=\"language-ruby\">def foo(x)\n  return 3\nend\n</code></pre>", "", context: "Example 142\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example143()
        {
            // Example 143
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ~~~~    ruby startline=3 $%@#$
            //     def foo(x)
            //       return 3
            //     end
            //     ~~~~~~~
            //
            // Should be rendered as:
            //     <pre><code class="language-ruby">def foo(x)
            //       return 3
            //     end
            //     </code></pre>

            TestParser.TestSpec("~~~~    ruby startline=3 $%@#$\ndef foo(x)\n  return 3\nend\n~~~~~~~", "<pre><code class=\"language-ruby\">def foo(x)\n  return 3\nend\n</code></pre>", "", context: "Example 143\nSection Leaf blocks / Fenced code blocks\n");
        }

        [Test]
        public void LeafBlocksFencedCodeBlocks_Example144()
        {
            // Example 144
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ````;
            //     ````
            //
            // Should be rendered as:
            //     <pre><code class="language-;"></code></pre>

            TestParser.TestSpec("````;\n````", "<pre><code class=\"language-;\"></code></pre>", "", context: "Example 144\nSection Leaf blocks / Fenced code blocks\n");
        }

        // [Info strings] for backtick code blocks cannot contain backticks:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example145()
        {
            // Example 145
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ``` aa ```
            //     foo
            //
            // Should be rendered as:
            //     <p><code>aa</code>
            //     foo</p>

            TestParser.TestSpec("``` aa ```\nfoo", "<p><code>aa</code>\nfoo</p>", "", context: "Example 145\nSection Leaf blocks / Fenced code blocks\n");
        }

        // [Info strings] for tilde code blocks can contain backticks and tildes:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example146()
        {
            // Example 146
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ~~~ aa ``` ~~~
            //     foo
            //     ~~~
            //
            // Should be rendered as:
            //     <pre><code class="language-aa">foo
            //     </code></pre>

            TestParser.TestSpec("~~~ aa ``` ~~~\nfoo\n~~~", "<pre><code class=\"language-aa\">foo\n</code></pre>", "", context: "Example 146\nSection Leaf blocks / Fenced code blocks\n");
        }

        // Closing code fences cannot have [info strings]:
        [Test]
        public void LeafBlocksFencedCodeBlocks_Example147()
        {
            // Example 147
            // Section: Leaf blocks / Fenced code blocks
            //
            // The following Markdown:
            //     ```
            //     ``` aaa
            //     ```
            //
            // Should be rendered as:
            //     <pre><code>``` aaa
            //     </code></pre>

            TestParser.TestSpec("```\n``` aaa\n```", "<pre><code>``` aaa\n</code></pre>", "", context: "Example 147\nSection Leaf blocks / Fenced code blocks\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksHTMLBlocks
    {
        // ## HTML blocks
        // 
        // An [HTML block](@) is a group of lines that is treated
        // as raw HTML (and will not be escaped in HTML output).
        // 
        // There are seven kinds of [HTML block], which can be defined by their
        // start and end conditions.  The block begins with a line that meets a
        // [start condition](@) (after up to three optional spaces of indentation).
        // It ends with the first subsequent line that meets a matching
        // [end condition](@), or the last line of the document, or the last line of
        // the [container block](#container-blocks) containing the current HTML
        // block, if no line is encountered that meets the [end condition].  If
        // the first line meets both the [start condition] and the [end
        // condition], the block will contain just that line.
        // 
        // 1.  **Start condition:**  line begins with the string `<pre`,
        // `<script`, `<style`, or `<textarea` (case-insensitive), followed by a space,
        // a tab, the string `>`, or the end of the line.\
        // **End condition:**  line contains an end tag
        // `</pre>`, `</script>`, `</style>`, or `</textarea>` (case-insensitive; it
        // need not match the start tag).
        // 
        // 2.  **Start condition:** line begins with the string `<!--`.\
        // **End condition:**  line contains the string `-->`.
        // 
        // 3.  **Start condition:** line begins with the string `<?`.\
        // **End condition:** line contains the string `?>`.
        // 
        // 4.  **Start condition:** line begins with the string `<!`
        // followed by an ASCII letter.\
        // **End condition:** line contains the character `>`.
        // 
        // 5.  **Start condition:**  line begins with the string
        // `<![CDATA[`.\
        // **End condition:** line contains the string `]]>`.
        // 
        // 6.  **Start condition:** line begins with the string `<` or `</`
        // followed by one of the strings (case-insensitive) `address`,
        // `article`, `aside`, `base`, `basefont`, `blockquote`, `body`,
        // `caption`, `center`, `col`, `colgroup`, `dd`, `details`, `dialog`,
        // `dir`, `div`, `dl`, `dt`, `fieldset`, `figcaption`, `figure`,
        // `footer`, `form`, `frame`, `frameset`,
        // `h1`, `h2`, `h3`, `h4`, `h5`, `h6`, `head`, `header`, `hr`,
        // `html`, `iframe`, `legend`, `li`, `link`, `main`, `menu`, `menuitem`,
        // `nav`, `noframes`, `ol`, `optgroup`, `option`, `p`, `param`,
        // `search`, `section`, `summary`, `table`, `tbody`, `td`,
        // `tfoot`, `th`, `thead`, `title`, `tr`, `track`, `ul`, followed
        // by a space, a tab, the end of the line, the string `>`, or
        // the string `/>`.\
        // **End condition:** line is followed by a [blank line].
        // 
        // 7.  **Start condition:**  line begins with a complete [open tag]
        // (with any [tag name] other than `pre`, `script`,
        // `style`, or `textarea`) or a complete [closing tag],
        // followed by zero or more spaces and tabs, followed by the end of the line.\
        // **End condition:** line is followed by a [blank line].
        // 
        // HTML blocks continue until they are closed by their appropriate
        // [end condition], or the last line of the document or other [container
        // block](#container-blocks).  This means any HTML **within an HTML
        // block** that might otherwise be recognised as a start condition will
        // be ignored by the parser and passed through as-is, without changing
        // the parser's state.
        // 
        // For instance, `<pre>` within an HTML block started by `<table>` will not affect
        // the parser state; as the HTML block was started in by start condition 6, it
        // will end at any blank line. This can be surprising:
        [Test]
        public void LeafBlocksHTMLBlocks_Example148()
        {
            // Example 148
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <table><tr><td>
            //     <pre>
            //     **Hello**,
            //     
            //     _world_.
            //     </pre>
            //     </td></tr></table>
            //
            // Should be rendered as:
            //     <table><tr><td>
            //     <pre>
            //     **Hello**,
            //     <p><em>world</em>.
            //     </pre></p>
            //     </td></tr></table>

            TestParser.TestSpec("<table><tr><td>\n<pre>\n**Hello**,\n\n_world_.\n</pre>\n</td></tr></table>", "<table><tr><td>\n<pre>\n**Hello**,\n<p><em>world</em>.\n</pre></p>\n</td></tr></table>", "", context: "Example 148\nSection Leaf blocks / HTML blocks\n");
        }

        // In this case, the HTML block is terminated by the blank line — the `**Hello**`
        // text remains verbatim — and regular parsing resumes, with a paragraph,
        // emphasised `world` and inline and block HTML following.
        // 
        // All types of [HTML blocks] except type 7 may interrupt
        // a paragraph.  Blocks of type 7 may not interrupt a paragraph.
        // (This restriction is intended to prevent unwanted interpretation
        // of long tags inside a wrapped paragraph as starting HTML blocks.)
        // 
        // Some simple examples follow.  Here are some basic HTML blocks
        // of type 6:
        [Test]
        public void LeafBlocksHTMLBlocks_Example149()
        {
            // Example 149
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <table>
            //       <tr>
            //         <td>
            //                hi
            //         </td>
            //       </tr>
            //     </table>
            //     
            //     okay.
            //
            // Should be rendered as:
            //     <table>
            //       <tr>
            //         <td>
            //                hi
            //         </td>
            //       </tr>
            //     </table>
            //     <p>okay.</p>

            TestParser.TestSpec("<table>\n  <tr>\n    <td>\n           hi\n    </td>\n  </tr>\n</table>\n\nokay.", "<table>\n  <tr>\n    <td>\n           hi\n    </td>\n  </tr>\n</table>\n<p>okay.</p>", "", context: "Example 149\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example150()
        {
            // Example 150
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //      <div>
            //       *hello*
            //              <foo><a>
            //
            // Should be rendered as:
            //      <div>
            //       *hello*
            //              <foo><a>

            TestParser.TestSpec(" <div>\n  *hello*\n         <foo><a>", " <div>\n  *hello*\n         <foo><a>", "", context: "Example 150\nSection Leaf blocks / HTML blocks\n");
        }

        // A block can also start with a closing tag:
        [Test]
        public void LeafBlocksHTMLBlocks_Example151()
        {
            // Example 151
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     </div>
            //     *foo*
            //
            // Should be rendered as:
            //     </div>
            //     *foo*

            TestParser.TestSpec("</div>\n*foo*", "</div>\n*foo*", "", context: "Example 151\nSection Leaf blocks / HTML blocks\n");
        }

        // Here we have two HTML blocks with a Markdown paragraph between them:
        [Test]
        public void LeafBlocksHTMLBlocks_Example152()
        {
            // Example 152
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <DIV CLASS="foo">
            //     
            //     *Markdown*
            //     
            //     </DIV>
            //
            // Should be rendered as:
            //     <DIV CLASS="foo">
            //     <p><em>Markdown</em></p>
            //     </DIV>

            TestParser.TestSpec("<DIV CLASS=\"foo\">\n\n*Markdown*\n\n</DIV>", "<DIV CLASS=\"foo\">\n<p><em>Markdown</em></p>\n</DIV>", "", context: "Example 152\nSection Leaf blocks / HTML blocks\n");
        }

        // The tag on the first line can be partial, as long
        // as it is split where there would be whitespace:
        [Test]
        public void LeafBlocksHTMLBlocks_Example153()
        {
            // Example 153
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div id="foo"
            //       class="bar">
            //     </div>
            //
            // Should be rendered as:
            //     <div id="foo"
            //       class="bar">
            //     </div>

            TestParser.TestSpec("<div id=\"foo\"\n  class=\"bar\">\n</div>", "<div id=\"foo\"\n  class=\"bar\">\n</div>", "", context: "Example 153\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example154()
        {
            // Example 154
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div id="foo" class="bar
            //       baz">
            //     </div>
            //
            // Should be rendered as:
            //     <div id="foo" class="bar
            //       baz">
            //     </div>

            TestParser.TestSpec("<div id=\"foo\" class=\"bar\n  baz\">\n</div>", "<div id=\"foo\" class=\"bar\n  baz\">\n</div>", "", context: "Example 154\nSection Leaf blocks / HTML blocks\n");
        }

        // An open tag need not be closed:
        [Test]
        public void LeafBlocksHTMLBlocks_Example155()
        {
            // Example 155
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div>
            //     *foo*
            //     
            //     *bar*
            //
            // Should be rendered as:
            //     <div>
            //     *foo*
            //     <p><em>bar</em></p>

            TestParser.TestSpec("<div>\n*foo*\n\n*bar*", "<div>\n*foo*\n<p><em>bar</em></p>", "", context: "Example 155\nSection Leaf blocks / HTML blocks\n");
        }

        // A partial tag need not even be completed (garbage
        // in, garbage out):
        [Test]
        public void LeafBlocksHTMLBlocks_Example156()
        {
            // Example 156
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div id="foo"
            //     *hi*
            //
            // Should be rendered as:
            //     <div id="foo"
            //     *hi*

            TestParser.TestSpec("<div id=\"foo\"\n*hi*", "<div id=\"foo\"\n*hi*", "", context: "Example 156\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example157()
        {
            // Example 157
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div class
            //     foo
            //
            // Should be rendered as:
            //     <div class
            //     foo

            TestParser.TestSpec("<div class\nfoo", "<div class\nfoo", "", context: "Example 157\nSection Leaf blocks / HTML blocks\n");
        }

        // The initial tag doesn't even need to be a valid
        // tag, as long as it starts like one:
        [Test]
        public void LeafBlocksHTMLBlocks_Example158()
        {
            // Example 158
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div *???-&&&-<---
            //     *foo*
            //
            // Should be rendered as:
            //     <div *???-&&&-<---
            //     *foo*

            TestParser.TestSpec("<div *???-&&&-<---\n*foo*", "<div *???-&&&-<---\n*foo*", "", context: "Example 158\nSection Leaf blocks / HTML blocks\n");
        }

        // In type 6 blocks, the initial tag need not be on a line by
        // itself:
        [Test]
        public void LeafBlocksHTMLBlocks_Example159()
        {
            // Example 159
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div><a href="bar">*foo*</a></div>
            //
            // Should be rendered as:
            //     <div><a href="bar">*foo*</a></div>

            TestParser.TestSpec("<div><a href=\"bar\">*foo*</a></div>", "<div><a href=\"bar\">*foo*</a></div>", "", context: "Example 159\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example160()
        {
            // Example 160
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <table><tr><td>
            //     foo
            //     </td></tr></table>
            //
            // Should be rendered as:
            //     <table><tr><td>
            //     foo
            //     </td></tr></table>

            TestParser.TestSpec("<table><tr><td>\nfoo\n</td></tr></table>", "<table><tr><td>\nfoo\n</td></tr></table>", "", context: "Example 160\nSection Leaf blocks / HTML blocks\n");
        }

        // Everything until the next blank line or end of document
        // gets included in the HTML block.  So, in the following
        // example, what looks like a Markdown code block
        // is actually part of the HTML block, which continues until a blank
        // line or the end of the document is reached:
        [Test]
        public void LeafBlocksHTMLBlocks_Example161()
        {
            // Example 161
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div></div>
            //     ``` c
            //     int x = 33;
            //     ```
            //
            // Should be rendered as:
            //     <div></div>
            //     ``` c
            //     int x = 33;
            //     ```

            TestParser.TestSpec("<div></div>\n``` c\nint x = 33;\n```", "<div></div>\n``` c\nint x = 33;\n```", "", context: "Example 161\nSection Leaf blocks / HTML blocks\n");
        }

        // To start an [HTML block] with a tag that is *not* in the
        // list of block-level tags in (6), you must put the tag by
        // itself on the first line (and it must be complete):
        [Test]
        public void LeafBlocksHTMLBlocks_Example162()
        {
            // Example 162
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <a href="foo">
            //     *bar*
            //     </a>
            //
            // Should be rendered as:
            //     <a href="foo">
            //     *bar*
            //     </a>

            TestParser.TestSpec("<a href=\"foo\">\n*bar*\n</a>", "<a href=\"foo\">\n*bar*\n</a>", "", context: "Example 162\nSection Leaf blocks / HTML blocks\n");
        }

        // In type 7 blocks, the [tag name] can be anything:
        [Test]
        public void LeafBlocksHTMLBlocks_Example163()
        {
            // Example 163
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <Warning>
            //     *bar*
            //     </Warning>
            //
            // Should be rendered as:
            //     <Warning>
            //     *bar*
            //     </Warning>

            TestParser.TestSpec("<Warning>\n*bar*\n</Warning>", "<Warning>\n*bar*\n</Warning>", "", context: "Example 163\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example164()
        {
            // Example 164
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <i class="foo">
            //     *bar*
            //     </i>
            //
            // Should be rendered as:
            //     <i class="foo">
            //     *bar*
            //     </i>

            TestParser.TestSpec("<i class=\"foo\">\n*bar*\n</i>", "<i class=\"foo\">\n*bar*\n</i>", "", context: "Example 164\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example165()
        {
            // Example 165
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     </ins>
            //     *bar*
            //
            // Should be rendered as:
            //     </ins>
            //     *bar*

            TestParser.TestSpec("</ins>\n*bar*", "</ins>\n*bar*", "", context: "Example 165\nSection Leaf blocks / HTML blocks\n");
        }

        // These rules are designed to allow us to work with tags that
        // can function as either block-level or inline-level tags.
        // The `<del>` tag is a nice example.  We can surround content with
        // `<del>` tags in three different ways.  In this case, we get a raw
        // HTML block, because the `<del>` tag is on a line by itself:
        [Test]
        public void LeafBlocksHTMLBlocks_Example166()
        {
            // Example 166
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <del>
            //     *foo*
            //     </del>
            //
            // Should be rendered as:
            //     <del>
            //     *foo*
            //     </del>

            TestParser.TestSpec("<del>\n*foo*\n</del>", "<del>\n*foo*\n</del>", "", context: "Example 166\nSection Leaf blocks / HTML blocks\n");
        }

        // In this case, we get a raw HTML block that just includes
        // the `<del>` tag (because it ends with the following blank
        // line).  So the contents get interpreted as CommonMark:
        [Test]
        public void LeafBlocksHTMLBlocks_Example167()
        {
            // Example 167
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <del>
            //     
            //     *foo*
            //     
            //     </del>
            //
            // Should be rendered as:
            //     <del>
            //     <p><em>foo</em></p>
            //     </del>

            TestParser.TestSpec("<del>\n\n*foo*\n\n</del>", "<del>\n<p><em>foo</em></p>\n</del>", "", context: "Example 167\nSection Leaf blocks / HTML blocks\n");
        }

        // Finally, in this case, the `<del>` tags are interpreted
        // as [raw HTML] *inside* the CommonMark paragraph.  (Because
        // the tag is not on a line by itself, we get inline HTML
        // rather than an [HTML block].)
        [Test]
        public void LeafBlocksHTMLBlocks_Example168()
        {
            // Example 168
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <del>*foo*</del>
            //
            // Should be rendered as:
            //     <p><del><em>foo</em></del></p>

            TestParser.TestSpec("<del>*foo*</del>", "<p><del><em>foo</em></del></p>", "", context: "Example 168\nSection Leaf blocks / HTML blocks\n");
        }

        // HTML tags designed to contain literal content
        // (`pre`, `script`, `style`, `textarea`), comments, processing instructions,
        // and declarations are treated somewhat differently.
        // Instead of ending at the first blank line, these blocks
        // end at the first line containing a corresponding end tag.
        // As a result, these blocks can contain blank lines:
        // 
        // A pre tag (type 1):
        [Test]
        public void LeafBlocksHTMLBlocks_Example169()
        {
            // Example 169
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <pre language="haskell"><code>
            //     import Text.HTML.TagSoup
            //     
            //     main :: IO ()
            //     main = print $ parseTags tags
            //     </code></pre>
            //     okay
            //
            // Should be rendered as:
            //     <pre language="haskell"><code>
            //     import Text.HTML.TagSoup
            //     
            //     main :: IO ()
            //     main = print $ parseTags tags
            //     </code></pre>
            //     <p>okay</p>

            TestParser.TestSpec("<pre language=\"haskell\"><code>\nimport Text.HTML.TagSoup\n\nmain :: IO ()\nmain = print $ parseTags tags\n</code></pre>\nokay", "<pre language=\"haskell\"><code>\nimport Text.HTML.TagSoup\n\nmain :: IO ()\nmain = print $ parseTags tags\n</code></pre>\n<p>okay</p>", "", context: "Example 169\nSection Leaf blocks / HTML blocks\n");
        }

        // A script tag (type 1):
        [Test]
        public void LeafBlocksHTMLBlocks_Example170()
        {
            // Example 170
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <script type="text/javascript">
            //     // JavaScript example
            //     
            //     document.getElementById("demo").innerHTML = "Hello JavaScript!";
            //     </script>
            //     okay
            //
            // Should be rendered as:
            //     <script type="text/javascript">
            //     // JavaScript example
            //     
            //     document.getElementById("demo").innerHTML = "Hello JavaScript!";
            //     </script>
            //     <p>okay</p>

            TestParser.TestSpec("<script type=\"text/javascript\">\n// JavaScript example\n\ndocument.getElementById(\"demo\").innerHTML = \"Hello JavaScript!\";\n</script>\nokay", "<script type=\"text/javascript\">\n// JavaScript example\n\ndocument.getElementById(\"demo\").innerHTML = \"Hello JavaScript!\";\n</script>\n<p>okay</p>", "", context: "Example 170\nSection Leaf blocks / HTML blocks\n");
        }

        // A textarea tag (type 1):
        [Test]
        public void LeafBlocksHTMLBlocks_Example171()
        {
            // Example 171
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <textarea>
            //     
            //     *foo*
            //     
            //     _bar_
            //     
            //     </textarea>
            //
            // Should be rendered as:
            //     <textarea>
            //     
            //     *foo*
            //     
            //     _bar_
            //     
            //     </textarea>

            TestParser.TestSpec("<textarea>\n\n*foo*\n\n_bar_\n\n</textarea>", "<textarea>\n\n*foo*\n\n_bar_\n\n</textarea>", "", context: "Example 171\nSection Leaf blocks / HTML blocks\n");
        }

        // A style tag (type 1):
        [Test]
        public void LeafBlocksHTMLBlocks_Example172()
        {
            // Example 172
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <style
            //       type="text/css">
            //     h1 {color:red;}
            //     
            //     p {color:blue;}
            //     </style>
            //     okay
            //
            // Should be rendered as:
            //     <style
            //       type="text/css">
            //     h1 {color:red;}
            //     
            //     p {color:blue;}
            //     </style>
            //     <p>okay</p>

            TestParser.TestSpec("<style\n  type=\"text/css\">\nh1 {color:red;}\n\np {color:blue;}\n</style>\nokay", "<style\n  type=\"text/css\">\nh1 {color:red;}\n\np {color:blue;}\n</style>\n<p>okay</p>", "", context: "Example 172\nSection Leaf blocks / HTML blocks\n");
        }

        // If there is no matching end tag, the block will end at the
        // end of the document (or the enclosing [block quote][block quotes]
        // or [list item][list items]):
        [Test]
        public void LeafBlocksHTMLBlocks_Example173()
        {
            // Example 173
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <style
            //       type="text/css">
            //     
            //     foo
            //
            // Should be rendered as:
            //     <style
            //       type="text/css">
            //     
            //     foo

            TestParser.TestSpec("<style\n  type=\"text/css\">\n\nfoo", "<style\n  type=\"text/css\">\n\nfoo", "", context: "Example 173\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example174()
        {
            // Example 174
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     > <div>
            //     > foo
            //     
            //     bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <div>
            //     foo
            //     </blockquote>
            //     <p>bar</p>

            TestParser.TestSpec("> <div>\n> foo\n\nbar", "<blockquote>\n<div>\nfoo\n</blockquote>\n<p>bar</p>", "", context: "Example 174\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example175()
        {
            // Example 175
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     - <div>
            //     - foo
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <div>
            //     </li>
            //     <li>foo</li>
            //     </ul>

            TestParser.TestSpec("- <div>\n- foo", "<ul>\n<li>\n<div>\n</li>\n<li>foo</li>\n</ul>", "", context: "Example 175\nSection Leaf blocks / HTML blocks\n");
        }

        // The end tag can occur on the same line as the start tag:
        [Test]
        public void LeafBlocksHTMLBlocks_Example176()
        {
            // Example 176
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <style>p{color:red;}</style>
            //     *foo*
            //
            // Should be rendered as:
            //     <style>p{color:red;}</style>
            //     <p><em>foo</em></p>

            TestParser.TestSpec("<style>p{color:red;}</style>\n*foo*", "<style>p{color:red;}</style>\n<p><em>foo</em></p>", "", context: "Example 176\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example177()
        {
            // Example 177
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <!-- foo -->*bar*
            //     *baz*
            //
            // Should be rendered as:
            //     <!-- foo -->*bar*
            //     <p><em>baz</em></p>

            TestParser.TestSpec("<!-- foo -->*bar*\n*baz*", "<!-- foo -->*bar*\n<p><em>baz</em></p>", "", context: "Example 177\nSection Leaf blocks / HTML blocks\n");
        }

        // Note that anything on the last line after the
        // end tag will be included in the [HTML block]:
        [Test]
        public void LeafBlocksHTMLBlocks_Example178()
        {
            // Example 178
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <script>
            //     foo
            //     </script>1. *bar*
            //
            // Should be rendered as:
            //     <script>
            //     foo
            //     </script>1. *bar*

            TestParser.TestSpec("<script>\nfoo\n</script>1. *bar*", "<script>\nfoo\n</script>1. *bar*", "", context: "Example 178\nSection Leaf blocks / HTML blocks\n");
        }

        // A comment (type 2):
        [Test]
        public void LeafBlocksHTMLBlocks_Example179()
        {
            // Example 179
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <!-- Foo
            //     
            //     bar
            //        baz -->
            //     okay
            //
            // Should be rendered as:
            //     <!-- Foo
            //     
            //     bar
            //        baz -->
            //     <p>okay</p>

            TestParser.TestSpec("<!-- Foo\n\nbar\n   baz -->\nokay", "<!-- Foo\n\nbar\n   baz -->\n<p>okay</p>", "", context: "Example 179\nSection Leaf blocks / HTML blocks\n");
        }

        // A processing instruction (type 3):
        [Test]
        public void LeafBlocksHTMLBlocks_Example180()
        {
            // Example 180
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <?php
            //     
            //       echo '>';
            //     
            //     ?>
            //     okay
            //
            // Should be rendered as:
            //     <?php
            //     
            //       echo '>';
            //     
            //     ?>
            //     <p>okay</p>

            TestParser.TestSpec("<?php\n\n  echo '>';\n\n?>\nokay", "<?php\n\n  echo '>';\n\n?>\n<p>okay</p>", "", context: "Example 180\nSection Leaf blocks / HTML blocks\n");
        }

        // A declaration (type 4):
        [Test]
        public void LeafBlocksHTMLBlocks_Example181()
        {
            // Example 181
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <!DOCTYPE html>
            //
            // Should be rendered as:
            //     <!DOCTYPE html>

            TestParser.TestSpec("<!DOCTYPE html>", "<!DOCTYPE html>", "", context: "Example 181\nSection Leaf blocks / HTML blocks\n");
        }

        // CDATA (type 5):
        [Test]
        public void LeafBlocksHTMLBlocks_Example182()
        {
            // Example 182
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <![CDATA[
            //     function matchwo(a,b)
            //     {
            //       if (a < b && a < 0) then {
            //         return 1;
            //     
            //       } else {
            //     
            //         return 0;
            //       }
            //     }
            //     ]]>
            //     okay
            //
            // Should be rendered as:
            //     <![CDATA[
            //     function matchwo(a,b)
            //     {
            //       if (a < b && a < 0) then {
            //         return 1;
            //     
            //       } else {
            //     
            //         return 0;
            //       }
            //     }
            //     ]]>
            //     <p>okay</p>

            TestParser.TestSpec("<![CDATA[\nfunction matchwo(a,b)\n{\n  if (a < b && a < 0) then {\n    return 1;\n\n  } else {\n\n    return 0;\n  }\n}\n]]>\nokay", "<![CDATA[\nfunction matchwo(a,b)\n{\n  if (a < b && a < 0) then {\n    return 1;\n\n  } else {\n\n    return 0;\n  }\n}\n]]>\n<p>okay</p>", "", context: "Example 182\nSection Leaf blocks / HTML blocks\n");
        }

        // The opening tag can be preceded by up to three spaces of indentation, but not
        // four:
        [Test]
        public void LeafBlocksHTMLBlocks_Example183()
        {
            // Example 183
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //       <!-- foo -->
            //     
            //         <!-- foo -->
            //
            // Should be rendered as:
            //       <!-- foo -->
            //     <pre><code>&lt;!-- foo --&gt;
            //     </code></pre>

            TestParser.TestSpec("  <!-- foo -->\n\n    <!-- foo -->", "  <!-- foo -->\n<pre><code>&lt;!-- foo --&gt;\n</code></pre>", "", context: "Example 183\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example184()
        {
            // Example 184
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //       <div>
            //     
            //         <div>
            //
            // Should be rendered as:
            //       <div>
            //     <pre><code>&lt;div&gt;
            //     </code></pre>

            TestParser.TestSpec("  <div>\n\n    <div>", "  <div>\n<pre><code>&lt;div&gt;\n</code></pre>", "", context: "Example 184\nSection Leaf blocks / HTML blocks\n");
        }

        // An HTML block of types 1--6 can interrupt a paragraph, and need not be
        // preceded by a blank line.
        [Test]
        public void LeafBlocksHTMLBlocks_Example185()
        {
            // Example 185
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     Foo
            //     <div>
            //     bar
            //     </div>
            //
            // Should be rendered as:
            //     <p>Foo</p>
            //     <div>
            //     bar
            //     </div>

            TestParser.TestSpec("Foo\n<div>\nbar\n</div>", "<p>Foo</p>\n<div>\nbar\n</div>", "", context: "Example 185\nSection Leaf blocks / HTML blocks\n");
        }

        // However, a following blank line is needed, except at the end of
        // a document, and except for blocks of types 1--5, [above][HTML
        // block]:
        [Test]
        public void LeafBlocksHTMLBlocks_Example186()
        {
            // Example 186
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div>
            //     bar
            //     </div>
            //     *foo*
            //
            // Should be rendered as:
            //     <div>
            //     bar
            //     </div>
            //     *foo*

            TestParser.TestSpec("<div>\nbar\n</div>\n*foo*", "<div>\nbar\n</div>\n*foo*", "", context: "Example 186\nSection Leaf blocks / HTML blocks\n");
        }

        // HTML blocks of type 7 cannot interrupt a paragraph:
        [Test]
        public void LeafBlocksHTMLBlocks_Example187()
        {
            // Example 187
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     Foo
            //     <a href="bar">
            //     baz
            //
            // Should be rendered as:
            //     <p>Foo
            //     <a href="bar">
            //     baz</p>

            TestParser.TestSpec("Foo\n<a href=\"bar\">\nbaz", "<p>Foo\n<a href=\"bar\">\nbaz</p>", "", context: "Example 187\nSection Leaf blocks / HTML blocks\n");
        }

        // This rule differs from John Gruber's original Markdown syntax
        // specification, which says:
        // 
        // > The only restrictions are that block-level HTML elements —
        // > e.g. `<div>`, `<table>`, `<pre>`, `<p>`, etc. — must be separated from
        // > surrounding content by blank lines, and the start and end tags of the
        // > block should not be indented with spaces or tabs.
        // 
        // In some ways Gruber's rule is more restrictive than the one given
        // here:
        // 
        // - It requires that an HTML block be preceded by a blank line.
        // - It does not allow the start tag to be indented.
        // - It requires a matching end tag, which it also does not allow to
        //   be indented.
        // 
        // Most Markdown implementations (including some of Gruber's own) do not
        // respect all of these restrictions.
        // 
        // There is one respect, however, in which Gruber's rule is more liberal
        // than the one given here, since it allows blank lines to occur inside
        // an HTML block.  There are two reasons for disallowing them here.
        // First, it removes the need to parse balanced tags, which is
        // expensive and can require backtracking from the end of the document
        // if no matching end tag is found. Second, it provides a very simple
        // and flexible way of including Markdown content inside HTML tags:
        // simply separate the Markdown from the HTML using blank lines:
        // 
        // Compare:
        [Test]
        public void LeafBlocksHTMLBlocks_Example188()
        {
            // Example 188
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div>
            //     
            //     *Emphasized* text.
            //     
            //     </div>
            //
            // Should be rendered as:
            //     <div>
            //     <p><em>Emphasized</em> text.</p>
            //     </div>

            TestParser.TestSpec("<div>\n\n*Emphasized* text.\n\n</div>", "<div>\n<p><em>Emphasized</em> text.</p>\n</div>", "", context: "Example 188\nSection Leaf blocks / HTML blocks\n");
        }

        [Test]
        public void LeafBlocksHTMLBlocks_Example189()
        {
            // Example 189
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <div>
            //     *Emphasized* text.
            //     </div>
            //
            // Should be rendered as:
            //     <div>
            //     *Emphasized* text.
            //     </div>

            TestParser.TestSpec("<div>\n*Emphasized* text.\n</div>", "<div>\n*Emphasized* text.\n</div>", "", context: "Example 189\nSection Leaf blocks / HTML blocks\n");
        }

        // Some Markdown implementations have adopted a convention of
        // interpreting content inside tags as text if the open tag has
        // the attribute `markdown=1`.  The rule given above seems a simpler and
        // more elegant way of achieving the same expressive power, which is also
        // much simpler to parse.
        // 
        // The main potential drawback is that one can no longer paste HTML
        // blocks into Markdown documents with 100% reliability.  However,
        // *in most cases* this will work fine, because the blank lines in
        // HTML are usually followed by HTML block tags.  For example:
        [Test]
        public void LeafBlocksHTMLBlocks_Example190()
        {
            // Example 190
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <table>
            //     
            //     <tr>
            //     
            //     <td>
            //     Hi
            //     </td>
            //     
            //     </tr>
            //     
            //     </table>
            //
            // Should be rendered as:
            //     <table>
            //     <tr>
            //     <td>
            //     Hi
            //     </td>
            //     </tr>
            //     </table>

            TestParser.TestSpec("<table>\n\n<tr>\n\n<td>\nHi\n</td>\n\n</tr>\n\n</table>", "<table>\n<tr>\n<td>\nHi\n</td>\n</tr>\n</table>", "", context: "Example 190\nSection Leaf blocks / HTML blocks\n");
        }

        // There are problems, however, if the inner tags are indented
        // *and* separated by spaces, as then they will be interpreted as
        // an indented code block:
        [Test]
        public void LeafBlocksHTMLBlocks_Example191()
        {
            // Example 191
            // Section: Leaf blocks / HTML blocks
            //
            // The following Markdown:
            //     <table>
            //     
            //       <tr>
            //     
            //         <td>
            //           Hi
            //         </td>
            //     
            //       </tr>
            //     
            //     </table>
            //
            // Should be rendered as:
            //     <table>
            //       <tr>
            //     <pre><code>&lt;td&gt;
            //       Hi
            //     &lt;/td&gt;
            //     </code></pre>
            //       </tr>
            //     </table>

            TestParser.TestSpec("<table>\n\n  <tr>\n\n    <td>\n      Hi\n    </td>\n\n  </tr>\n\n</table>", "<table>\n  <tr>\n<pre><code>&lt;td&gt;\n  Hi\n&lt;/td&gt;\n</code></pre>\n  </tr>\n</table>", "", context: "Example 191\nSection Leaf blocks / HTML blocks\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksLinkReferenceDefinitions
    {
        // Fortunately, blank lines are usually not necessary and can be
        // deleted.  The exception is inside `<pre>` tags, but as described
        // [above][HTML blocks], raw HTML blocks starting with `<pre>`
        // *can* contain blank lines.
        // 
        // ## Link reference definitions
        // 
        // A [link reference definition](@)
        // consists of a [link label], optionally preceded by up to three spaces of
        // indentation, followed
        // by a colon (`:`), optional spaces or tabs (including up to one
        // [line ending]), a [link destination],
        // optional spaces or tabs (including up to one
        // [line ending]), and an optional [link
        // title], which if it is present must be separated
        // from the [link destination] by spaces or tabs.
        // No further character may occur.
        // 
        // A [link reference definition]
        // does not correspond to a structural element of a document.  Instead, it
        // defines a label which can be used in [reference links]
        // and reference-style [images] elsewhere in the document.  [Link
        // reference definitions] can come either before or after the links that use
        // them.
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example192()
        {
            // Example 192
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url "title"
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">foo</a></p>

            TestParser.TestSpec("[foo]: /url \"title\"\n\n[foo]", "<p><a href=\"/url\" title=\"title\">foo</a></p>", "", context: "Example 192\nSection Leaf blocks / Link reference definitions\n");
        }

        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example193()
        {
            // Example 193
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //        [foo]: 
            //           /url  
            //                'the title'  
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p><a href="/url" title="the title">foo</a></p>

            TestParser.TestSpec("   [foo]: \n      /url  \n           'the title'  \n\n[foo]", "<p><a href=\"/url\" title=\"the title\">foo</a></p>", "", context: "Example 193\nSection Leaf blocks / Link reference definitions\n");
        }

        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example194()
        {
            // Example 194
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [Foo*bar\]]:my_(url) 'title (with parens)'
            //     
            //     [Foo*bar\]]
            //
            // Should be rendered as:
            //     <p><a href="my_(url)" title="title (with parens)">Foo*bar]</a></p>

            TestParser.TestSpec("[Foo*bar\\]]:my_(url) 'title (with parens)'\n\n[Foo*bar\\]]", "<p><a href=\"my_(url)\" title=\"title (with parens)\">Foo*bar]</a></p>", "", context: "Example 194\nSection Leaf blocks / Link reference definitions\n");
        }

        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example195()
        {
            // Example 195
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [Foo bar]:
            //     <my url>
            //     'title'
            //     
            //     [Foo bar]
            //
            // Should be rendered as:
            //     <p><a href="my%20url" title="title">Foo bar</a></p>

            TestParser.TestSpec("[Foo bar]:\n<my url>\n'title'\n\n[Foo bar]", "<p><a href=\"my%20url\" title=\"title\">Foo bar</a></p>", "", context: "Example 195\nSection Leaf blocks / Link reference definitions\n");
        }

        // The title may extend over multiple lines:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example196()
        {
            // Example 196
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url '
            //     title
            //     line1
            //     line2
            //     '
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p><a href="/url" title="
            //     title
            //     line1
            //     line2
            //     ">foo</a></p>

            TestParser.TestSpec("[foo]: /url '\ntitle\nline1\nline2\n'\n\n[foo]", "<p><a href=\"/url\" title=\"\ntitle\nline1\nline2\n\">foo</a></p>", "", context: "Example 196\nSection Leaf blocks / Link reference definitions\n");
        }

        // However, it may not contain a [blank line]:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example197()
        {
            // Example 197
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url 'title
            //     
            //     with blank line'
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p>[foo]: /url 'title</p>
            //     <p>with blank line'</p>
            //     <p>[foo]</p>

            TestParser.TestSpec("[foo]: /url 'title\n\nwith blank line'\n\n[foo]", "<p>[foo]: /url 'title</p>\n<p>with blank line'</p>\n<p>[foo]</p>", "", context: "Example 197\nSection Leaf blocks / Link reference definitions\n");
        }

        // The title may be omitted:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example198()
        {
            // Example 198
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]:
            //     /url
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p><a href="/url">foo</a></p>

            TestParser.TestSpec("[foo]:\n/url\n\n[foo]", "<p><a href=\"/url\">foo</a></p>", "", context: "Example 198\nSection Leaf blocks / Link reference definitions\n");
        }

        // The link destination may not be omitted:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example199()
        {
            // Example 199
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]:
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p>[foo]:</p>
            //     <p>[foo]</p>

            TestParser.TestSpec("[foo]:\n\n[foo]", "<p>[foo]:</p>\n<p>[foo]</p>", "", context: "Example 199\nSection Leaf blocks / Link reference definitions\n");
        }

        //  However, an empty link destination may be specified using
        //  angle brackets:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example200()
        {
            // Example 200
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: <>
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p><a href="">foo</a></p>

            TestParser.TestSpec("[foo]: <>\n\n[foo]", "<p><a href=\"\">foo</a></p>", "", context: "Example 200\nSection Leaf blocks / Link reference definitions\n");
        }

        // The title must be separated from the link destination by
        // spaces or tabs:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example201()
        {
            // Example 201
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: <bar>(baz)
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p>[foo]: <bar>(baz)</p>
            //     <p>[foo]</p>

            TestParser.TestSpec("[foo]: <bar>(baz)\n\n[foo]", "<p>[foo]: <bar>(baz)</p>\n<p>[foo]</p>", "", context: "Example 201\nSection Leaf blocks / Link reference definitions\n");
        }

        // Both title and destination can contain backslash escapes
        // and literal backslashes:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example202()
        {
            // Example 202
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url\bar\*baz "foo\"bar\baz"
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <p><a href="/url%5Cbar*baz" title="foo&quot;bar\baz">foo</a></p>

            TestParser.TestSpec("[foo]: /url\\bar\\*baz \"foo\\\"bar\\baz\"\n\n[foo]", "<p><a href=\"/url%5Cbar*baz\" title=\"foo&quot;bar\\baz\">foo</a></p>", "", context: "Example 202\nSection Leaf blocks / Link reference definitions\n");
        }

        // A link can come before its corresponding definition:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example203()
        {
            // Example 203
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]
            //     
            //     [foo]: url
            //
            // Should be rendered as:
            //     <p><a href="url">foo</a></p>

            TestParser.TestSpec("[foo]\n\n[foo]: url", "<p><a href=\"url\">foo</a></p>", "", context: "Example 203\nSection Leaf blocks / Link reference definitions\n");
        }

        // If there are several matching definitions, the first one takes
        // precedence:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example204()
        {
            // Example 204
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]
            //     
            //     [foo]: first
            //     [foo]: second
            //
            // Should be rendered as:
            //     <p><a href="first">foo</a></p>

            TestParser.TestSpec("[foo]\n\n[foo]: first\n[foo]: second", "<p><a href=\"first\">foo</a></p>", "", context: "Example 204\nSection Leaf blocks / Link reference definitions\n");
        }

        // As noted in the section on [Links], matching of labels is
        // case-insensitive (see [matches]).
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example205()
        {
            // Example 205
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [FOO]: /url
            //     
            //     [Foo]
            //
            // Should be rendered as:
            //     <p><a href="/url">Foo</a></p>

            TestParser.TestSpec("[FOO]: /url\n\n[Foo]", "<p><a href=\"/url\">Foo</a></p>", "", context: "Example 205\nSection Leaf blocks / Link reference definitions\n");
        }

        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example206()
        {
            // Example 206
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [ΑΓΩ]: /φου
            //     
            //     [αγω]
            //
            // Should be rendered as:
            //     <p><a href="/%CF%86%CE%BF%CF%85">αγω</a></p>

            TestParser.TestSpec("[ΑΓΩ]: /φου\n\n[αγω]", "<p><a href=\"/%CF%86%CE%BF%CF%85\">αγω</a></p>", "", context: "Example 206\nSection Leaf blocks / Link reference definitions\n");
        }

        // Whether something is a [link reference definition] is
        // independent of whether the link reference it defines is
        // used in the document.  Thus, for example, the following
        // document contains just a link reference definition, and
        // no visible content:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example207()
        {
            // Example 207
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url
            //
            // Should be rendered as:
            //
            TestParser.TestSpec("[foo]: /url", "", "", context: "Example 207\nSection Leaf blocks / Link reference definitions\n");
        }

        // Here is another one:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example208()
        {
            // Example 208
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [
            //     foo
            //     ]: /url
            //     bar
            //
            // Should be rendered as:
            //     <p>bar</p>

            TestParser.TestSpec("[\nfoo\n]: /url\nbar", "<p>bar</p>", "", context: "Example 208\nSection Leaf blocks / Link reference definitions\n");
        }

        // This is not a link reference definition, because there are
        // characters other than spaces or tabs after the title:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example209()
        {
            // Example 209
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url "title" ok
            //
            // Should be rendered as:
            //     <p>[foo]: /url &quot;title&quot; ok</p>

            TestParser.TestSpec("[foo]: /url \"title\" ok", "<p>[foo]: /url &quot;title&quot; ok</p>", "", context: "Example 209\nSection Leaf blocks / Link reference definitions\n");
        }

        // This is a link reference definition, but it has no title:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example210()
        {
            // Example 210
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url
            //     "title" ok
            //
            // Should be rendered as:
            //     <p>&quot;title&quot; ok</p>

            TestParser.TestSpec("[foo]: /url\n\"title\" ok", "<p>&quot;title&quot; ok</p>", "", context: "Example 210\nSection Leaf blocks / Link reference definitions\n");
        }

        // This is not a link reference definition, because it is indented
        // four spaces:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example211()
        {
            // Example 211
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //         [foo]: /url "title"
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <pre><code>[foo]: /url &quot;title&quot;
            //     </code></pre>
            //     <p>[foo]</p>

            TestParser.TestSpec("    [foo]: /url \"title\"\n\n[foo]", "<pre><code>[foo]: /url &quot;title&quot;\n</code></pre>\n<p>[foo]</p>", "", context: "Example 211\nSection Leaf blocks / Link reference definitions\n");
        }

        // This is not a link reference definition, because it occurs inside
        // a code block:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example212()
        {
            // Example 212
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     ```
            //     [foo]: /url
            //     ```
            //     
            //     [foo]
            //
            // Should be rendered as:
            //     <pre><code>[foo]: /url
            //     </code></pre>
            //     <p>[foo]</p>

            TestParser.TestSpec("```\n[foo]: /url\n```\n\n[foo]", "<pre><code>[foo]: /url\n</code></pre>\n<p>[foo]</p>", "", context: "Example 212\nSection Leaf blocks / Link reference definitions\n");
        }

        // A [link reference definition] cannot interrupt a paragraph.
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example213()
        {
            // Example 213
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     Foo
            //     [bar]: /baz
            //     
            //     [bar]
            //
            // Should be rendered as:
            //     <p>Foo
            //     [bar]: /baz</p>
            //     <p>[bar]</p>

            TestParser.TestSpec("Foo\n[bar]: /baz\n\n[bar]", "<p>Foo\n[bar]: /baz</p>\n<p>[bar]</p>", "", context: "Example 213\nSection Leaf blocks / Link reference definitions\n");
        }

        // However, it can directly follow other block elements, such as headings
        // and thematic breaks, and it need not be followed by a blank line.
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example214()
        {
            // Example 214
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     # [Foo]
            //     [foo]: /url
            //     > bar
            //
            // Should be rendered as:
            //     <h1><a href="/url">Foo</a></h1>
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>

            TestParser.TestSpec("# [Foo]\n[foo]: /url\n> bar", "<h1><a href=\"/url\">Foo</a></h1>\n<blockquote>\n<p>bar</p>\n</blockquote>", "", context: "Example 214\nSection Leaf blocks / Link reference definitions\n");
        }

        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example215()
        {
            // Example 215
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url
            //     bar
            //     ===
            //     [foo]
            //
            // Should be rendered as:
            //     <h1>bar</h1>
            //     <p><a href="/url">foo</a></p>

            TestParser.TestSpec("[foo]: /url\nbar\n===\n[foo]", "<h1>bar</h1>\n<p><a href=\"/url\">foo</a></p>", "", context: "Example 215\nSection Leaf blocks / Link reference definitions\n");
        }

        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example216()
        {
            // Example 216
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /url
            //     ===
            //     [foo]
            //
            // Should be rendered as:
            //     <p>===
            //     <a href="/url">foo</a></p>

            TestParser.TestSpec("[foo]: /url\n===\n[foo]", "<p>===\n<a href=\"/url\">foo</a></p>", "", context: "Example 216\nSection Leaf blocks / Link reference definitions\n");
        }

        // Several [link reference definitions]
        // can occur one after another, without intervening blank lines.
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example217()
        {
            // Example 217
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]: /foo-url "foo"
            //     [bar]: /bar-url
            //       "bar"
            //     [baz]: /baz-url
            //     
            //     [foo],
            //     [bar],
            //     [baz]
            //
            // Should be rendered as:
            //     <p><a href="/foo-url" title="foo">foo</a>,
            //     <a href="/bar-url" title="bar">bar</a>,
            //     <a href="/baz-url">baz</a></p>

            TestParser.TestSpec("[foo]: /foo-url \"foo\"\n[bar]: /bar-url\n  \"bar\"\n[baz]: /baz-url\n\n[foo],\n[bar],\n[baz]", "<p><a href=\"/foo-url\" title=\"foo\">foo</a>,\n<a href=\"/bar-url\" title=\"bar\">bar</a>,\n<a href=\"/baz-url\">baz</a></p>", "", context: "Example 217\nSection Leaf blocks / Link reference definitions\n");
        }

        // [Link reference definitions] can occur
        // inside block containers, like lists and block quotations.  They
        // affect the entire document, not just the container in which they
        // are defined:
        [Test]
        public void LeafBlocksLinkReferenceDefinitions_Example218()
        {
            // Example 218
            // Section: Leaf blocks / Link reference definitions
            //
            // The following Markdown:
            //     [foo]
            //     
            //     > [foo]: /url
            //
            // Should be rendered as:
            //     <p><a href="/url">foo</a></p>
            //     <blockquote>
            //     </blockquote>

            TestParser.TestSpec("[foo]\n\n> [foo]: /url", "<p><a href=\"/url\">foo</a></p>\n<blockquote>\n</blockquote>", "", context: "Example 218\nSection Leaf blocks / Link reference definitions\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksParagraphs
    {
        // ## Paragraphs
        // 
        // A sequence of non-blank lines that cannot be interpreted as other
        // kinds of blocks forms a [paragraph](@).
        // The contents of the paragraph are the result of parsing the
        // paragraph's raw content as inlines.  The paragraph's raw content
        // is formed by concatenating the lines and removing initial and final
        // spaces or tabs.
        // 
        // A simple example with two paragraphs:
        [Test]
        public void LeafBlocksParagraphs_Example219()
        {
            // Example 219
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //     aaa
            //     
            //     bbb
            //
            // Should be rendered as:
            //     <p>aaa</p>
            //     <p>bbb</p>

            TestParser.TestSpec("aaa\n\nbbb", "<p>aaa</p>\n<p>bbb</p>", "", context: "Example 219\nSection Leaf blocks / Paragraphs\n");
        }

        // Paragraphs can contain multiple lines, but no blank lines:
        [Test]
        public void LeafBlocksParagraphs_Example220()
        {
            // Example 220
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //     aaa
            //     bbb
            //     
            //     ccc
            //     ddd
            //
            // Should be rendered as:
            //     <p>aaa
            //     bbb</p>
            //     <p>ccc
            //     ddd</p>

            TestParser.TestSpec("aaa\nbbb\n\nccc\nddd", "<p>aaa\nbbb</p>\n<p>ccc\nddd</p>", "", context: "Example 220\nSection Leaf blocks / Paragraphs\n");
        }

        // Multiple blank lines between paragraphs have no effect:
        [Test]
        public void LeafBlocksParagraphs_Example221()
        {
            // Example 221
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //     aaa
            //     
            //     
            //     bbb
            //
            // Should be rendered as:
            //     <p>aaa</p>
            //     <p>bbb</p>

            TestParser.TestSpec("aaa\n\n\nbbb", "<p>aaa</p>\n<p>bbb</p>", "", context: "Example 221\nSection Leaf blocks / Paragraphs\n");
        }

        // Leading spaces or tabs are skipped:
        [Test]
        public void LeafBlocksParagraphs_Example222()
        {
            // Example 222
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //       aaa
            //      bbb
            //
            // Should be rendered as:
            //     <p>aaa
            //     bbb</p>

            TestParser.TestSpec("  aaa\n bbb", "<p>aaa\nbbb</p>", "", context: "Example 222\nSection Leaf blocks / Paragraphs\n");
        }

        // Lines after the first may be indented any amount, since indented
        // code blocks cannot interrupt paragraphs.
        [Test]
        public void LeafBlocksParagraphs_Example223()
        {
            // Example 223
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //     aaa
            //                  bbb
            //                                            ccc
            //
            // Should be rendered as:
            //     <p>aaa
            //     bbb
            //     ccc</p>

            TestParser.TestSpec("aaa\n             bbb\n                                       ccc", "<p>aaa\nbbb\nccc</p>", "", context: "Example 223\nSection Leaf blocks / Paragraphs\n");
        }

        // However, the first line may be preceded by up to three spaces of indentation.
        // Four spaces of indentation is too many:
        [Test]
        public void LeafBlocksParagraphs_Example224()
        {
            // Example 224
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //        aaa
            //     bbb
            //
            // Should be rendered as:
            //     <p>aaa
            //     bbb</p>

            TestParser.TestSpec("   aaa\nbbb", "<p>aaa\nbbb</p>", "", context: "Example 224\nSection Leaf blocks / Paragraphs\n");
        }

        [Test]
        public void LeafBlocksParagraphs_Example225()
        {
            // Example 225
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //         aaa
            //     bbb
            //
            // Should be rendered as:
            //     <pre><code>aaa
            //     </code></pre>
            //     <p>bbb</p>

            TestParser.TestSpec("    aaa\nbbb", "<pre><code>aaa\n</code></pre>\n<p>bbb</p>", "", context: "Example 225\nSection Leaf blocks / Paragraphs\n");
        }

        // Final spaces or tabs are stripped before inline parsing, so a paragraph
        // that ends with two or more spaces will not end with a [hard line
        // break]:
        [Test]
        public void LeafBlocksParagraphs_Example226()
        {
            // Example 226
            // Section: Leaf blocks / Paragraphs
            //
            // The following Markdown:
            //     aaa     
            //     bbb     
            //
            // Should be rendered as:
            //     <p>aaa<br />
            //     bbb</p>

            TestParser.TestSpec("aaa     \nbbb     ", "<p>aaa<br />\nbbb</p>", "", context: "Example 226\nSection Leaf blocks / Paragraphs\n");
        }
    }

    [TestFixture]
    public class TestLeafBlocksBlankLines
    {
        // ## Blank lines
        // 
        // [Blank lines] between block-level elements are ignored,
        // except for the role they play in determining whether a [list]
        // is [tight] or [loose].
        // 
        // Blank lines at the beginning and end of the document are also ignored.
        [Test]
        public void LeafBlocksBlankLines_Example227()
        {
            // Example 227
            // Section: Leaf blocks / Blank lines
            //
            // The following Markdown:
            //       
            //     
            //     aaa
            //       
            //     
            //     # aaa
            //     
            //       
            //
            // Should be rendered as:
            //     <p>aaa</p>
            //     <h1>aaa</h1>

            TestParser.TestSpec("  \n\naaa\n  \n\n# aaa\n\n  ", "<p>aaa</p>\n<h1>aaa</h1>", "", context: "Example 227\nSection Leaf blocks / Blank lines\n");
        }
    }

    [TestFixture]
    public class TestContainerBlocksBlockQuotes
    {
        // # Container blocks
        // 
        // A [container block](#container-blocks) is a block that has other
        // blocks as its contents.  There are two basic kinds of container blocks:
        // [block quotes] and [list items].
        // [Lists] are meta-containers for [list items].
        // 
        // We define the syntax for container blocks recursively.  The general
        // form of the definition is:
        // 
        // > If X is a sequence of blocks, then the result of
        // > transforming X in such-and-such a way is a container of type Y
        // > with these blocks as its content.
        // 
        // So, we explain what counts as a block quote or list item by explaining
        // how these can be *generated* from their contents. This should suffice
        // to define the syntax, although it does not give a recipe for *parsing*
        // these constructions.  (A recipe is provided below in the section entitled
        // [A parsing strategy](#appendix-a-parsing-strategy).)
        // 
        // ## Block quotes
        // 
        // A [block quote marker](@),
        // optionally preceded by up to three spaces of indentation,
        // consists of (a) the character `>` together with a following space of
        // indentation, or (b) a single character `>` not followed by a space of
        // indentation.
        // 
        // The following rules define [block quotes]:
        // 
        // 1.  **Basic case.**  If a string of lines *Ls* constitute a sequence
        //     of blocks *Bs*, then the result of prepending a [block quote
        //     marker] to the beginning of each line in *Ls*
        //     is a [block quote](#block-quotes) containing *Bs*.
        // 
        // 2.  **Laziness.**  If a string of lines *Ls* constitute a [block
        //     quote](#block-quotes) with contents *Bs*, then the result of deleting
        //     the initial [block quote marker] from one or
        //     more lines in which the next character other than a space or tab after the
        //     [block quote marker] is [paragraph continuation
        //     text] is a block quote with *Bs* as its content.
        //     [Paragraph continuation text](@) is text
        //     that will be parsed as part of the content of a paragraph, but does
        //     not occur at the beginning of the paragraph.
        // 
        // 3.  **Consecutiveness.**  A document cannot contain two [block
        //     quotes] in a row unless there is a [blank line] between them.
        // 
        // Nothing else counts as a [block quote](#block-quotes).
        // 
        // Here is a simple example:
        [Test]
        public void ContainerBlocksBlockQuotes_Example228()
        {
            // Example 228
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > # Foo
            //     > bar
            //     > baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>

            TestParser.TestSpec("> # Foo\n> bar\n> baz", "<blockquote>\n<h1>Foo</h1>\n<p>bar\nbaz</p>\n</blockquote>", "", context: "Example 228\nSection Container blocks / Block quotes\n");
        }

        // The space or tab after the `>` characters can be omitted:
        [Test]
        public void ContainerBlocksBlockQuotes_Example229()
        {
            // Example 229
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     ># Foo
            //     >bar
            //     > baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>

            TestParser.TestSpec("># Foo\n>bar\n> baz", "<blockquote>\n<h1>Foo</h1>\n<p>bar\nbaz</p>\n</blockquote>", "", context: "Example 229\nSection Container blocks / Block quotes\n");
        }

        // The `>` characters can be preceded by up to three spaces of indentation:
        [Test]
        public void ContainerBlocksBlockQuotes_Example230()
        {
            // Example 230
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //        > # Foo
            //        > bar
            //      > baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>

            TestParser.TestSpec("   > # Foo\n   > bar\n > baz", "<blockquote>\n<h1>Foo</h1>\n<p>bar\nbaz</p>\n</blockquote>", "", context: "Example 230\nSection Container blocks / Block quotes\n");
        }

        // Four spaces of indentation is too many:
        [Test]
        public void ContainerBlocksBlockQuotes_Example231()
        {
            // Example 231
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //         > # Foo
            //         > bar
            //         > baz
            //
            // Should be rendered as:
            //     <pre><code>&gt; # Foo
            //     &gt; bar
            //     &gt; baz
            //     </code></pre>

            TestParser.TestSpec("    > # Foo\n    > bar\n    > baz", "<pre><code>&gt; # Foo\n&gt; bar\n&gt; baz\n</code></pre>", "", context: "Example 231\nSection Container blocks / Block quotes\n");
        }

        // The Laziness clause allows us to omit the `>` before
        // [paragraph continuation text]:
        [Test]
        public void ContainerBlocksBlockQuotes_Example232()
        {
            // Example 232
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > # Foo
            //     > bar
            //     baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>

            TestParser.TestSpec("> # Foo\n> bar\nbaz", "<blockquote>\n<h1>Foo</h1>\n<p>bar\nbaz</p>\n</blockquote>", "", context: "Example 232\nSection Container blocks / Block quotes\n");
        }

        // A block quote can contain some lazy and some non-lazy
        // continuation lines:
        [Test]
        public void ContainerBlocksBlockQuotes_Example233()
        {
            // Example 233
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > bar
            //     baz
            //     > foo
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>bar
            //     baz
            //     foo</p>
            //     </blockquote>

            TestParser.TestSpec("> bar\nbaz\n> foo", "<blockquote>\n<p>bar\nbaz\nfoo</p>\n</blockquote>", "", context: "Example 233\nSection Container blocks / Block quotes\n");
        }

        // Laziness only applies to lines that would have been continuations of
        // paragraphs had they been prepended with [block quote markers].
        // For example, the `> ` cannot be omitted in the second line of
        // 
        // ``` markdown
        // > foo
        // > ---
        // ```
        // 
        // without changing the meaning:
        [Test]
        public void ContainerBlocksBlockQuotes_Example234()
        {
            // Example 234
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > foo
            //     ---
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>
            //     <hr />

            TestParser.TestSpec("> foo\n---", "<blockquote>\n<p>foo</p>\n</blockquote>\n<hr />", "", context: "Example 234\nSection Container blocks / Block quotes\n");
        }

        // Similarly, if we omit the `> ` in the second line of
        // 
        // ``` markdown
        // > - foo
        // > - bar
        // ```
        // 
        // then the block quote ends after the first line:
        [Test]
        public void ContainerBlocksBlockQuotes_Example235()
        {
            // Example 235
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > - foo
            //     - bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     </blockquote>
            //     <ul>
            //     <li>bar</li>
            //     </ul>

            TestParser.TestSpec("> - foo\n- bar", "<blockquote>\n<ul>\n<li>foo</li>\n</ul>\n</blockquote>\n<ul>\n<li>bar</li>\n</ul>", "", context: "Example 235\nSection Container blocks / Block quotes\n");
        }

        // For the same reason, we can't omit the `> ` in front of
        // subsequent lines of an indented or fenced code block:
        [Test]
        public void ContainerBlocksBlockQuotes_Example236()
        {
            // Example 236
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     >     foo
            //         bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <pre><code>foo
            //     </code></pre>
            //     </blockquote>
            //     <pre><code>bar
            //     </code></pre>

            TestParser.TestSpec(">     foo\n    bar", "<blockquote>\n<pre><code>foo\n</code></pre>\n</blockquote>\n<pre><code>bar\n</code></pre>", "", context: "Example 236\nSection Container blocks / Block quotes\n");
        }

        [Test]
        public void ContainerBlocksBlockQuotes_Example237()
        {
            // Example 237
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > ```
            //     foo
            //     ```
            //
            // Should be rendered as:
            //     <blockquote>
            //     <pre><code></code></pre>
            //     </blockquote>
            //     <p>foo</p>
            //     <pre><code></code></pre>

            TestParser.TestSpec("> ```\nfoo\n```", "<blockquote>\n<pre><code></code></pre>\n</blockquote>\n<p>foo</p>\n<pre><code></code></pre>", "", context: "Example 237\nSection Container blocks / Block quotes\n");
        }

        // Note that in the following case, we have a [lazy
        // continuation line]:
        [Test]
        public void ContainerBlocksBlockQuotes_Example238()
        {
            // Example 238
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > foo
            //         - bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo
            //     - bar</p>
            //     </blockquote>

            TestParser.TestSpec("> foo\n    - bar", "<blockquote>\n<p>foo\n- bar</p>\n</blockquote>", "", context: "Example 238\nSection Container blocks / Block quotes\n");
        }

        // To see why, note that in
        // 
        // ```markdown
        // > foo
        // >     - bar
        // ```
        // 
        // the `- bar` is indented too far to start a list, and can't
        // be an indented code block because indented code blocks cannot
        // interrupt paragraphs, so it is [paragraph continuation text].
        // 
        // A block quote can be empty:
        [Test]
        public void ContainerBlocksBlockQuotes_Example239()
        {
            // Example 239
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     >
            //
            // Should be rendered as:
            //     <blockquote>
            //     </blockquote>

            TestParser.TestSpec(">", "<blockquote>\n</blockquote>", "", context: "Example 239\nSection Container blocks / Block quotes\n");
        }

        [Test]
        public void ContainerBlocksBlockQuotes_Example240()
        {
            // Example 240
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     >
            //     >  
            //     > 
            //
            // Should be rendered as:
            //     <blockquote>
            //     </blockquote>

            TestParser.TestSpec(">\n>  \n> ", "<blockquote>\n</blockquote>", "", context: "Example 240\nSection Container blocks / Block quotes\n");
        }

        // A block quote can have initial or final blank lines:
        [Test]
        public void ContainerBlocksBlockQuotes_Example241()
        {
            // Example 241
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     >
            //     > foo
            //     >  
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>

            TestParser.TestSpec(">\n> foo\n>  ", "<blockquote>\n<p>foo</p>\n</blockquote>", "", context: "Example 241\nSection Container blocks / Block quotes\n");
        }

        // A blank line always separates block quotes:
        [Test]
        public void ContainerBlocksBlockQuotes_Example242()
        {
            // Example 242
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > foo
            //     
            //     > bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>

            TestParser.TestSpec("> foo\n\n> bar", "<blockquote>\n<p>foo</p>\n</blockquote>\n<blockquote>\n<p>bar</p>\n</blockquote>", "", context: "Example 242\nSection Container blocks / Block quotes\n");
        }

        // (Most current Markdown implementations, including John Gruber's
        // original `Markdown.pl`, will parse this example as a single block quote
        // with two paragraphs.  But it seems better to allow the author to decide
        // whether two block quotes or one are wanted.)
        // 
        // Consecutiveness means that if we put these block quotes together,
        // we get a single block quote:
        [Test]
        public void ContainerBlocksBlockQuotes_Example243()
        {
            // Example 243
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > foo
            //     > bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo
            //     bar</p>
            //     </blockquote>

            TestParser.TestSpec("> foo\n> bar", "<blockquote>\n<p>foo\nbar</p>\n</blockquote>", "", context: "Example 243\nSection Container blocks / Block quotes\n");
        }

        // To get a block quote with two paragraphs, use:
        [Test]
        public void ContainerBlocksBlockQuotes_Example244()
        {
            // Example 244
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > foo
            //     >
            //     > bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </blockquote>

            TestParser.TestSpec("> foo\n>\n> bar", "<blockquote>\n<p>foo</p>\n<p>bar</p>\n</blockquote>", "", context: "Example 244\nSection Container blocks / Block quotes\n");
        }

        // Block quotes can interrupt paragraphs:
        [Test]
        public void ContainerBlocksBlockQuotes_Example245()
        {
            // Example 245
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     foo
            //     > bar
            //
            // Should be rendered as:
            //     <p>foo</p>
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>

            TestParser.TestSpec("foo\n> bar", "<p>foo</p>\n<blockquote>\n<p>bar</p>\n</blockquote>", "", context: "Example 245\nSection Container blocks / Block quotes\n");
        }

        // In general, blank lines are not needed before or after block
        // quotes:
        [Test]
        public void ContainerBlocksBlockQuotes_Example246()
        {
            // Example 246
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > aaa
            //     ***
            //     > bbb
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>aaa</p>
            //     </blockquote>
            //     <hr />
            //     <blockquote>
            //     <p>bbb</p>
            //     </blockquote>

            TestParser.TestSpec("> aaa\n***\n> bbb", "<blockquote>\n<p>aaa</p>\n</blockquote>\n<hr />\n<blockquote>\n<p>bbb</p>\n</blockquote>", "", context: "Example 246\nSection Container blocks / Block quotes\n");
        }

        // However, because of laziness, a blank line is needed between
        // a block quote and a following paragraph:
        [Test]
        public void ContainerBlocksBlockQuotes_Example247()
        {
            // Example 247
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > bar
            //     baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>bar
            //     baz</p>
            //     </blockquote>

            TestParser.TestSpec("> bar\nbaz", "<blockquote>\n<p>bar\nbaz</p>\n</blockquote>", "", context: "Example 247\nSection Container blocks / Block quotes\n");
        }

        [Test]
        public void ContainerBlocksBlockQuotes_Example248()
        {
            // Example 248
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > bar
            //     
            //     baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>
            //     <p>baz</p>

            TestParser.TestSpec("> bar\n\nbaz", "<blockquote>\n<p>bar</p>\n</blockquote>\n<p>baz</p>", "", context: "Example 248\nSection Container blocks / Block quotes\n");
        }

        [Test]
        public void ContainerBlocksBlockQuotes_Example249()
        {
            // Example 249
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > bar
            //     >
            //     baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>
            //     <p>baz</p>

            TestParser.TestSpec("> bar\n>\nbaz", "<blockquote>\n<p>bar</p>\n</blockquote>\n<p>baz</p>", "", context: "Example 249\nSection Container blocks / Block quotes\n");
        }

        // It is a consequence of the Laziness rule that any number
        // of initial `>`s may be omitted on a continuation line of a
        // nested block quote:
        [Test]
        public void ContainerBlocksBlockQuotes_Example250()
        {
            // Example 250
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     > > > foo
            //     bar
            //
            // Should be rendered as:
            //     <blockquote>
            //     <blockquote>
            //     <blockquote>
            //     <p>foo
            //     bar</p>
            //     </blockquote>
            //     </blockquote>
            //     </blockquote>

            TestParser.TestSpec("> > > foo\nbar", "<blockquote>\n<blockquote>\n<blockquote>\n<p>foo\nbar</p>\n</blockquote>\n</blockquote>\n</blockquote>", "", context: "Example 250\nSection Container blocks / Block quotes\n");
        }

        [Test]
        public void ContainerBlocksBlockQuotes_Example251()
        {
            // Example 251
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     >>> foo
            //     > bar
            //     >>baz
            //
            // Should be rendered as:
            //     <blockquote>
            //     <blockquote>
            //     <blockquote>
            //     <p>foo
            //     bar
            //     baz</p>
            //     </blockquote>
            //     </blockquote>
            //     </blockquote>

            TestParser.TestSpec(">>> foo\n> bar\n>>baz", "<blockquote>\n<blockquote>\n<blockquote>\n<p>foo\nbar\nbaz</p>\n</blockquote>\n</blockquote>\n</blockquote>", "", context: "Example 251\nSection Container blocks / Block quotes\n");
        }

        // When including an indented code block in a block quote,
        // remember that the [block quote marker] includes
        // both the `>` and a following space of indentation.  So *five spaces* are needed
        // after the `>`:
        [Test]
        public void ContainerBlocksBlockQuotes_Example252()
        {
            // Example 252
            // Section: Container blocks / Block quotes
            //
            // The following Markdown:
            //     >     code
            //     
            //     >    not code
            //
            // Should be rendered as:
            //     <blockquote>
            //     <pre><code>code
            //     </code></pre>
            //     </blockquote>
            //     <blockquote>
            //     <p>not code</p>
            //     </blockquote>

            TestParser.TestSpec(">     code\n\n>    not code", "<blockquote>\n<pre><code>code\n</code></pre>\n</blockquote>\n<blockquote>\n<p>not code</p>\n</blockquote>", "", context: "Example 252\nSection Container blocks / Block quotes\n");
        }
    }

    [TestFixture]
    public class TestContainerBlocksListItems
    {
        // ## List items
        // 
        // A [list marker](@) is a
        // [bullet list marker] or an [ordered list marker].
        // 
        // A [bullet list marker](@)
        // is a `-`, `+`, or `*` character.
        // 
        // An [ordered list marker](@)
        // is a sequence of 1--9 arabic digits (`0-9`), followed by either a
        // `.` character or a `)` character.  (The reason for the length
        // limit is that with 10 digits we start seeing integer overflows
        // in some browsers.)
        // 
        // The following rules define [list items]:
        // 
        // 1.  **Basic case.**  If a sequence of lines *Ls* constitute a sequence of
        //     blocks *Bs* starting with a character other than a space or tab, and *M* is
        //     a list marker of width *W* followed by 1 ≤ *N* ≤ 4 spaces of indentation,
        //     then the result of prepending *M* and the following spaces to the first line
        //     of *Ls*, and indenting subsequent lines of *Ls* by *W + N* spaces, is a
        //     list item with *Bs* as its contents.  The type of the list item
        //     (bullet or ordered) is determined by the type of its list marker.
        //     If the list item is ordered, then it is also assigned a start
        //     number, based on the ordered list marker.
        // 
        //     Exceptions:
        // 
        //     1. When the first list item in a [list] interrupts
        //        a paragraph---that is, when it starts on a line that would
        //        otherwise count as [paragraph continuation text]---then (a)
        //        the lines *Ls* must not begin with a blank line, and (b) if
        //        the list item is ordered, the start number must be 1.
        //     2. If any line is a [thematic break][thematic breaks] then
        //        that line is not a list item.
        // 
        // For example, let *Ls* be the lines
        [Test]
        public void ContainerBlocksListItems_Example253()
        {
            // Example 253
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     A paragraph
            //     with two lines.
            //     
            //         indented code
            //     
            //     > A block quote.
            //
            // Should be rendered as:
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>

            TestParser.TestSpec("A paragraph\nwith two lines.\n\n    indented code\n\n> A block quote.", "<p>A paragraph\nwith two lines.</p>\n<pre><code>indented code\n</code></pre>\n<blockquote>\n<p>A block quote.</p>\n</blockquote>", "", context: "Example 253\nSection Container blocks / List items\n");
        }

        // And let *M* be the marker `1.`, and *N* = 2.  Then rule #1 says
        // that the following is an ordered list item with start number 1,
        // and the same contents as *Ls*:
        [Test]
        public void ContainerBlocksListItems_Example254()
        {
            // Example 254
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     1.  A paragraph
            //         with two lines.
            //     
            //             indented code
            //     
            //         > A block quote.
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1.  A paragraph\n    with two lines.\n\n        indented code\n\n    > A block quote.", "<ol>\n<li>\n<p>A paragraph\nwith two lines.</p>\n<pre><code>indented code\n</code></pre>\n<blockquote>\n<p>A block quote.</p>\n</blockquote>\n</li>\n</ol>", "", context: "Example 254\nSection Container blocks / List items\n");
        }

        // The most important thing to notice is that the position of
        // the text after the list marker determines how much indentation
        // is needed in subsequent blocks in the list item.  If the list
        // marker takes up two spaces of indentation, and there are three spaces between
        // the list marker and the next character other than a space or tab, then blocks
        // must be indented five spaces in order to fall under the list
        // item.
        // 
        // Here are some examples showing how far content must be indented to be
        // put under the list item:
        [Test]
        public void ContainerBlocksListItems_Example255()
        {
            // Example 255
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - one
            //     
            //      two
            //
            // Should be rendered as:
            //     <ul>
            //     <li>one</li>
            //     </ul>
            //     <p>two</p>

            TestParser.TestSpec("- one\n\n two", "<ul>\n<li>one</li>\n</ul>\n<p>two</p>", "", context: "Example 255\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example256()
        {
            // Example 256
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - one
            //     
            //       two
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>one</p>
            //     <p>two</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- one\n\n  two", "<ul>\n<li>\n<p>one</p>\n<p>two</p>\n</li>\n</ul>", "", context: "Example 256\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example257()
        {
            // Example 257
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //      -    one
            //     
            //          two
            //
            // Should be rendered as:
            //     <ul>
            //     <li>one</li>
            //     </ul>
            //     <pre><code> two
            //     </code></pre>

            TestParser.TestSpec(" -    one\n\n     two", "<ul>\n<li>one</li>\n</ul>\n<pre><code> two\n</code></pre>", "", context: "Example 257\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example258()
        {
            // Example 258
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //      -    one
            //     
            //           two
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>one</p>
            //     <p>two</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec(" -    one\n\n      two", "<ul>\n<li>\n<p>one</p>\n<p>two</p>\n</li>\n</ul>", "", context: "Example 258\nSection Container blocks / List items\n");
        }

        // It is tempting to think of this in terms of columns:  the continuation
        // blocks must be indented at least to the column of the first character other than
        // a space or tab after the list marker.  However, that is not quite right.
        // The spaces of indentation after the list marker determine how much relative
        // indentation is needed.  Which column this indentation reaches will depend on
        // how the list item is embedded in other constructions, as shown by
        // this example:
        [Test]
        public void ContainerBlocksListItems_Example259()
        {
            // Example 259
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //        > > 1.  one
            //     >>
            //     >>     two
            //
            // Should be rendered as:
            //     <blockquote>
            //     <blockquote>
            //     <ol>
            //     <li>
            //     <p>one</p>
            //     <p>two</p>
            //     </li>
            //     </ol>
            //     </blockquote>
            //     </blockquote>

            TestParser.TestSpec("   > > 1.  one\n>>\n>>     two", "<blockquote>\n<blockquote>\n<ol>\n<li>\n<p>one</p>\n<p>two</p>\n</li>\n</ol>\n</blockquote>\n</blockquote>", "", context: "Example 259\nSection Container blocks / List items\n");
        }

        // Here `two` occurs in the same column as the list marker `1.`,
        // but is actually contained in the list item, because there is
        // sufficient indentation after the last containing blockquote marker.
        // 
        // The converse is also possible.  In the following example, the word `two`
        // occurs far to the right of the initial text of the list item, `one`, but
        // it is not considered part of the list item, because it is not indented
        // far enough past the blockquote marker:
        [Test]
        public void ContainerBlocksListItems_Example260()
        {
            // Example 260
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     >>- one
            //     >>
            //       >  > two
            //
            // Should be rendered as:
            //     <blockquote>
            //     <blockquote>
            //     <ul>
            //     <li>one</li>
            //     </ul>
            //     <p>two</p>
            //     </blockquote>
            //     </blockquote>

            TestParser.TestSpec(">>- one\n>>\n  >  > two", "<blockquote>\n<blockquote>\n<ul>\n<li>one</li>\n</ul>\n<p>two</p>\n</blockquote>\n</blockquote>", "", context: "Example 260\nSection Container blocks / List items\n");
        }

        // Note that at least one space or tab is needed between the list marker and
        // any following content, so these are not list items:
        [Test]
        public void ContainerBlocksListItems_Example261()
        {
            // Example 261
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     -one
            //     
            //     2.two
            //
            // Should be rendered as:
            //     <p>-one</p>
            //     <p>2.two</p>

            TestParser.TestSpec("-one\n\n2.two", "<p>-one</p>\n<p>2.two</p>", "", context: "Example 261\nSection Container blocks / List items\n");
        }

        // A list item may contain blocks that are separated by more than
        // one blank line.
        [Test]
        public void ContainerBlocksListItems_Example262()
        {
            // Example 262
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - foo
            //     
            //     
            //       bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- foo\n\n\n  bar", "<ul>\n<li>\n<p>foo</p>\n<p>bar</p>\n</li>\n</ul>", "", context: "Example 262\nSection Container blocks / List items\n");
        }

        // A list item may contain any kind of block:
        [Test]
        public void ContainerBlocksListItems_Example263()
        {
            // Example 263
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     1.  foo
            //     
            //         ```
            //         bar
            //         ```
            //     
            //         baz
            //     
            //         > bam
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     <p>baz</p>
            //     <blockquote>
            //     <p>bam</p>
            //     </blockquote>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1.  foo\n\n    ```\n    bar\n    ```\n\n    baz\n\n    > bam", "<ol>\n<li>\n<p>foo</p>\n<pre><code>bar\n</code></pre>\n<p>baz</p>\n<blockquote>\n<p>bam</p>\n</blockquote>\n</li>\n</ol>", "", context: "Example 263\nSection Container blocks / List items\n");
        }

        // A list item that contains an indented code block will preserve
        // empty lines within the code block verbatim.
        [Test]
        public void ContainerBlocksListItems_Example264()
        {
            // Example 264
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - Foo
            //     
            //           bar
            //     
            //     
            //           baz
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>Foo</p>
            //     <pre><code>bar
            //     
            //     
            //     baz
            //     </code></pre>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- Foo\n\n      bar\n\n\n      baz", "<ul>\n<li>\n<p>Foo</p>\n<pre><code>bar\n\n\nbaz\n</code></pre>\n</li>\n</ul>", "", context: "Example 264\nSection Container blocks / List items\n");
        }

        // Note that ordered list start numbers must be nine digits or less:
        [Test]
        public void ContainerBlocksListItems_Example265()
        {
            // Example 265
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     123456789. ok
            //
            // Should be rendered as:
            //     <ol start="123456789">
            //     <li>ok</li>
            //     </ol>

            TestParser.TestSpec("123456789. ok", "<ol start=\"123456789\">\n<li>ok</li>\n</ol>", "", context: "Example 265\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example266()
        {
            // Example 266
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     1234567890. not ok
            //
            // Should be rendered as:
            //     <p>1234567890. not ok</p>

            TestParser.TestSpec("1234567890. not ok", "<p>1234567890. not ok</p>", "", context: "Example 266\nSection Container blocks / List items\n");
        }

        // A start number may begin with 0s:
        [Test]
        public void ContainerBlocksListItems_Example267()
        {
            // Example 267
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     0. ok
            //
            // Should be rendered as:
            //     <ol start="0">
            //     <li>ok</li>
            //     </ol>

            TestParser.TestSpec("0. ok", "<ol start=\"0\">\n<li>ok</li>\n</ol>", "", context: "Example 267\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example268()
        {
            // Example 268
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     003. ok
            //
            // Should be rendered as:
            //     <ol start="3">
            //     <li>ok</li>
            //     </ol>

            TestParser.TestSpec("003. ok", "<ol start=\"3\">\n<li>ok</li>\n</ol>", "", context: "Example 268\nSection Container blocks / List items\n");
        }

        // A start number may not be negative:
        [Test]
        public void ContainerBlocksListItems_Example269()
        {
            // Example 269
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     -1. not ok
            //
            // Should be rendered as:
            //     <p>-1. not ok</p>

            TestParser.TestSpec("-1. not ok", "<p>-1. not ok</p>", "", context: "Example 269\nSection Container blocks / List items\n");
        }

        // 2.  **Item starting with indented code.**  If a sequence of lines *Ls*
        //     constitute a sequence of blocks *Bs* starting with an indented code
        //     block, and *M* is a list marker of width *W* followed by
        //     one space of indentation, then the result of prepending *M* and the
        //     following space to the first line of *Ls*, and indenting subsequent lines
        //     of *Ls* by *W + 1* spaces, is a list item with *Bs* as its contents.
        //     If a line is empty, then it need not be indented.  The type of the
        //     list item (bullet or ordered) is determined by the type of its list
        //     marker.  If the list item is ordered, then it is also assigned a
        //     start number, based on the ordered list marker.
        // 
        // An indented code block will have to be preceded by four spaces of indentation
        // beyond the edge of the region where text will be included in the list item.
        // In the following case that is 6 spaces:
        [Test]
        public void ContainerBlocksListItems_Example270()
        {
            // Example 270
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - foo
            //     
            //           bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- foo\n\n      bar", "<ul>\n<li>\n<p>foo</p>\n<pre><code>bar\n</code></pre>\n</li>\n</ul>", "", context: "Example 270\nSection Container blocks / List items\n");
        }

        // And in this case it is 11 spaces:
        [Test]
        public void ContainerBlocksListItems_Example271()
        {
            // Example 271
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //       10.  foo
            //     
            //                bar
            //
            // Should be rendered as:
            //     <ol start="10">
            //     <li>
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     </li>
            //     </ol>

            TestParser.TestSpec("  10.  foo\n\n           bar", "<ol start=\"10\">\n<li>\n<p>foo</p>\n<pre><code>bar\n</code></pre>\n</li>\n</ol>", "", context: "Example 271\nSection Container blocks / List items\n");
        }

        // If the *first* block in the list item is an indented code block,
        // then by rule #2, the contents must be preceded by *one* space of indentation
        // after the list marker:
        [Test]
        public void ContainerBlocksListItems_Example272()
        {
            // Example 272
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //         indented code
            //     
            //     paragraph
            //     
            //         more code
            //
            // Should be rendered as:
            //     <pre><code>indented code
            //     </code></pre>
            //     <p>paragraph</p>
            //     <pre><code>more code
            //     </code></pre>

            TestParser.TestSpec("    indented code\n\nparagraph\n\n    more code", "<pre><code>indented code\n</code></pre>\n<p>paragraph</p>\n<pre><code>more code\n</code></pre>", "", context: "Example 272\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example273()
        {
            // Example 273
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     1.     indented code
            //     
            //        paragraph
            //     
            //            more code
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <pre><code>indented code
            //     </code></pre>
            //     <p>paragraph</p>
            //     <pre><code>more code
            //     </code></pre>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1.     indented code\n\n   paragraph\n\n       more code", "<ol>\n<li>\n<pre><code>indented code\n</code></pre>\n<p>paragraph</p>\n<pre><code>more code\n</code></pre>\n</li>\n</ol>", "", context: "Example 273\nSection Container blocks / List items\n");
        }

        // Note that an additional space of indentation is interpreted as space
        // inside the code block:
        [Test]
        public void ContainerBlocksListItems_Example274()
        {
            // Example 274
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     1.      indented code
            //     
            //        paragraph
            //     
            //            more code
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <pre><code> indented code
            //     </code></pre>
            //     <p>paragraph</p>
            //     <pre><code>more code
            //     </code></pre>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1.      indented code\n\n   paragraph\n\n       more code", "<ol>\n<li>\n<pre><code> indented code\n</code></pre>\n<p>paragraph</p>\n<pre><code>more code\n</code></pre>\n</li>\n</ol>", "", context: "Example 274\nSection Container blocks / List items\n");
        }

        // Note that rules #1 and #2 only apply to two cases:  (a) cases
        // in which the lines to be included in a list item begin with a
        // character other than a space or tab, and (b) cases in which
        // they begin with an indented code
        // block.  In a case like the following, where the first block begins with
        // three spaces of indentation, the rules do not allow us to form a list item by
        // indenting the whole thing and prepending a list marker:
        [Test]
        public void ContainerBlocksListItems_Example275()
        {
            // Example 275
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //        foo
            //     
            //     bar
            //
            // Should be rendered as:
            //     <p>foo</p>
            //     <p>bar</p>

            TestParser.TestSpec("   foo\n\nbar", "<p>foo</p>\n<p>bar</p>", "", context: "Example 275\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example276()
        {
            // Example 276
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     -    foo
            //     
            //       bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     <p>bar</p>

            TestParser.TestSpec("-    foo\n\n  bar", "<ul>\n<li>foo</li>\n</ul>\n<p>bar</p>", "", context: "Example 276\nSection Container blocks / List items\n");
        }

        // This is not a significant restriction, because when a block is preceded by up to
        // three spaces of indentation, the indentation can always be removed without
        // a change in interpretation, allowing rule #1 to be applied.  So, in
        // the above case:
        [Test]
        public void ContainerBlocksListItems_Example277()
        {
            // Example 277
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     -  foo
            //     
            //        bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("-  foo\n\n   bar", "<ul>\n<li>\n<p>foo</p>\n<p>bar</p>\n</li>\n</ul>", "", context: "Example 277\nSection Container blocks / List items\n");
        }

        // 3.  **Item starting with a blank line.**  If a sequence of lines *Ls*
        //     starting with a single [blank line] constitute a (possibly empty)
        //     sequence of blocks *Bs*, and *M* is a list marker of width *W*,
        //     then the result of prepending *M* to the first line of *Ls*, and
        //     preceding subsequent lines of *Ls* by *W + 1* spaces of indentation, is a
        //     list item with *Bs* as its contents.
        //     If a line is empty, then it need not be indented.  The type of the
        //     list item (bullet or ordered) is determined by the type of its list
        //     marker.  If the list item is ordered, then it is also assigned a
        //     start number, based on the ordered list marker.
        // 
        // Here are some list items that start with a blank line but are not empty:
        [Test]
        public void ContainerBlocksListItems_Example278()
        {
            // Example 278
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     -
            //       foo
            //     -
            //       ```
            //       bar
            //       ```
            //     -
            //           baz
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     <li>
            //     <pre><code>bar
            //     </code></pre>
            //     </li>
            //     <li>
            //     <pre><code>baz
            //     </code></pre>
            //     </li>
            //     </ul>

            TestParser.TestSpec("-\n  foo\n-\n  ```\n  bar\n  ```\n-\n      baz", "<ul>\n<li>foo</li>\n<li>\n<pre><code>bar\n</code></pre>\n</li>\n<li>\n<pre><code>baz\n</code></pre>\n</li>\n</ul>", "", context: "Example 278\nSection Container blocks / List items\n");
        }

        // When the list item starts with a blank line, the number of spaces
        // following the list marker doesn't change the required indentation:
        [Test]
        public void ContainerBlocksListItems_Example279()
        {
            // Example 279
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     -   
            //       foo
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     </ul>

            TestParser.TestSpec("-   \n  foo", "<ul>\n<li>foo</li>\n</ul>", "", context: "Example 279\nSection Container blocks / List items\n");
        }

        // A list item can begin with at most one blank line.
        // In the following example, `foo` is not part of the list
        // item:
        [Test]
        public void ContainerBlocksListItems_Example280()
        {
            // Example 280
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     -
            //     
            //       foo
            //
            // Should be rendered as:
            //     <ul>
            //     <li></li>
            //     </ul>
            //     <p>foo</p>

            TestParser.TestSpec("-\n\n  foo", "<ul>\n<li></li>\n</ul>\n<p>foo</p>", "", context: "Example 280\nSection Container blocks / List items\n");
        }

        // Here is an empty bullet list item:
        [Test]
        public void ContainerBlocksListItems_Example281()
        {
            // Example 281
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - foo
            //     -
            //     - bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     <li></li>
            //     <li>bar</li>
            //     </ul>

            TestParser.TestSpec("- foo\n-\n- bar", "<ul>\n<li>foo</li>\n<li></li>\n<li>bar</li>\n</ul>", "", context: "Example 281\nSection Container blocks / List items\n");
        }

        // It does not matter whether there are spaces or tabs following the [list marker]:
        [Test]
        public void ContainerBlocksListItems_Example282()
        {
            // Example 282
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - foo
            //     -   
            //     - bar
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     <li></li>
            //     <li>bar</li>
            //     </ul>

            TestParser.TestSpec("- foo\n-   \n- bar", "<ul>\n<li>foo</li>\n<li></li>\n<li>bar</li>\n</ul>", "", context: "Example 282\nSection Container blocks / List items\n");
        }

        // Here is an empty ordered list item:
        [Test]
        public void ContainerBlocksListItems_Example283()
        {
            // Example 283
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     1. foo
            //     2.
            //     3. bar
            //
            // Should be rendered as:
            //     <ol>
            //     <li>foo</li>
            //     <li></li>
            //     <li>bar</li>
            //     </ol>

            TestParser.TestSpec("1. foo\n2.\n3. bar", "<ol>\n<li>foo</li>\n<li></li>\n<li>bar</li>\n</ol>", "", context: "Example 283\nSection Container blocks / List items\n");
        }

        // A list may start or end with an empty list item:
        [Test]
        public void ContainerBlocksListItems_Example284()
        {
            // Example 284
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     *
            //
            // Should be rendered as:
            //     <ul>
            //     <li></li>
            //     </ul>

            TestParser.TestSpec("*", "<ul>\n<li></li>\n</ul>", "", context: "Example 284\nSection Container blocks / List items\n");
        }

        // However, an empty list item cannot interrupt a paragraph:
        [Test]
        public void ContainerBlocksListItems_Example285()
        {
            // Example 285
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     foo
            //     *
            //     
            //     foo
            //     1.
            //
            // Should be rendered as:
            //     <p>foo
            //     *</p>
            //     <p>foo
            //     1.</p>

            TestParser.TestSpec("foo\n*\n\nfoo\n1.", "<p>foo\n*</p>\n<p>foo\n1.</p>", "", context: "Example 285\nSection Container blocks / List items\n");
        }

        // 4.  **Indentation.**  If a sequence of lines *Ls* constitutes a list item
        //     according to rule #1, #2, or #3, then the result of preceding each line
        //     of *Ls* by up to three spaces of indentation (the same for each line) also
        //     constitutes a list item with the same contents and attributes.  If a line is
        //     empty, then it need not be indented.
        // 
        // Indented one space:
        [Test]
        public void ContainerBlocksListItems_Example286()
        {
            // Example 286
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //      1.  A paragraph
            //          with two lines.
            //     
            //              indented code
            //     
            //          > A block quote.
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>

            TestParser.TestSpec(" 1.  A paragraph\n     with two lines.\n\n         indented code\n\n     > A block quote.", "<ol>\n<li>\n<p>A paragraph\nwith two lines.</p>\n<pre><code>indented code\n</code></pre>\n<blockquote>\n<p>A block quote.</p>\n</blockquote>\n</li>\n</ol>", "", context: "Example 286\nSection Container blocks / List items\n");
        }

        // Indented two spaces:
        [Test]
        public void ContainerBlocksListItems_Example287()
        {
            // Example 287
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //       1.  A paragraph
            //           with two lines.
            //     
            //               indented code
            //     
            //           > A block quote.
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>

            TestParser.TestSpec("  1.  A paragraph\n      with two lines.\n\n          indented code\n\n      > A block quote.", "<ol>\n<li>\n<p>A paragraph\nwith two lines.</p>\n<pre><code>indented code\n</code></pre>\n<blockquote>\n<p>A block quote.</p>\n</blockquote>\n</li>\n</ol>", "", context: "Example 287\nSection Container blocks / List items\n");
        }

        // Indented three spaces:
        [Test]
        public void ContainerBlocksListItems_Example288()
        {
            // Example 288
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //        1.  A paragraph
            //            with two lines.
            //     
            //                indented code
            //     
            //            > A block quote.
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>

            TestParser.TestSpec("   1.  A paragraph\n       with two lines.\n\n           indented code\n\n       > A block quote.", "<ol>\n<li>\n<p>A paragraph\nwith two lines.</p>\n<pre><code>indented code\n</code></pre>\n<blockquote>\n<p>A block quote.</p>\n</blockquote>\n</li>\n</ol>", "", context: "Example 288\nSection Container blocks / List items\n");
        }

        // Four spaces indent gives a code block:
        [Test]
        public void ContainerBlocksListItems_Example289()
        {
            // Example 289
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //         1.  A paragraph
            //             with two lines.
            //     
            //                 indented code
            //     
            //             > A block quote.
            //
            // Should be rendered as:
            //     <pre><code>1.  A paragraph
            //         with two lines.
            //     
            //             indented code
            //     
            //         &gt; A block quote.
            //     </code></pre>

            TestParser.TestSpec("    1.  A paragraph\n        with two lines.\n\n            indented code\n\n        > A block quote.", "<pre><code>1.  A paragraph\n    with two lines.\n\n        indented code\n\n    &gt; A block quote.\n</code></pre>", "", context: "Example 289\nSection Container blocks / List items\n");
        }

        // 5.  **Laziness.**  If a string of lines *Ls* constitute a [list
        //     item](#list-items) with contents *Bs*, then the result of deleting
        //     some or all of the indentation from one or more lines in which the
        //     next character other than a space or tab after the indentation is
        //     [paragraph continuation text] is a
        //     list item with the same contents and attributes.  The unindented
        //     lines are called
        //     [lazy continuation line](@)s.
        // 
        // Here is an example with [lazy continuation lines]:
        [Test]
        public void ContainerBlocksListItems_Example290()
        {
            // Example 290
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //       1.  A paragraph
            //     with two lines.
            //     
            //               indented code
            //     
            //           > A block quote.
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>

            TestParser.TestSpec("  1.  A paragraph\nwith two lines.\n\n          indented code\n\n      > A block quote.", "<ol>\n<li>\n<p>A paragraph\nwith two lines.</p>\n<pre><code>indented code\n</code></pre>\n<blockquote>\n<p>A block quote.</p>\n</blockquote>\n</li>\n</ol>", "", context: "Example 290\nSection Container blocks / List items\n");
        }

        // Indentation can be partially deleted:
        [Test]
        public void ContainerBlocksListItems_Example291()
        {
            // Example 291
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //       1.  A paragraph
            //         with two lines.
            //
            // Should be rendered as:
            //     <ol>
            //     <li>A paragraph
            //     with two lines.</li>
            //     </ol>

            TestParser.TestSpec("  1.  A paragraph\n    with two lines.", "<ol>\n<li>A paragraph\nwith two lines.</li>\n</ol>", "", context: "Example 291\nSection Container blocks / List items\n");
        }

        // These examples show how laziness can work in nested structures:
        [Test]
        public void ContainerBlocksListItems_Example292()
        {
            // Example 292
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     > 1. > Blockquote
            //     continued here.
            //
            // Should be rendered as:
            //     <blockquote>
            //     <ol>
            //     <li>
            //     <blockquote>
            //     <p>Blockquote
            //     continued here.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            //     </blockquote>

            TestParser.TestSpec("> 1. > Blockquote\ncontinued here.", "<blockquote>\n<ol>\n<li>\n<blockquote>\n<p>Blockquote\ncontinued here.</p>\n</blockquote>\n</li>\n</ol>\n</blockquote>", "", context: "Example 292\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example293()
        {
            // Example 293
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     > 1. > Blockquote
            //     > continued here.
            //
            // Should be rendered as:
            //     <blockquote>
            //     <ol>
            //     <li>
            //     <blockquote>
            //     <p>Blockquote
            //     continued here.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            //     </blockquote>

            TestParser.TestSpec("> 1. > Blockquote\n> continued here.", "<blockquote>\n<ol>\n<li>\n<blockquote>\n<p>Blockquote\ncontinued here.</p>\n</blockquote>\n</li>\n</ol>\n</blockquote>", "", context: "Example 293\nSection Container blocks / List items\n");
        }

        // 6.  **That's all.** Nothing that is not counted as a list item by rules
        //     #1--5 counts as a [list item](#list-items).
        // 
        // The rules for sublists follow from the general rules
        // [above][List items].  A sublist must be indented the same number
        // of spaces of indentation a paragraph would need to be in order to be included
        // in the list item.
        // 
        // So, in this case we need two spaces indent:
        [Test]
        public void ContainerBlocksListItems_Example294()
        {
            // Example 294
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - foo
            //       - bar
            //         - baz
            //           - boo
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo
            //     <ul>
            //     <li>bar
            //     <ul>
            //     <li>baz
            //     <ul>
            //     <li>boo</li>
            //     </ul>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- foo\n  - bar\n    - baz\n      - boo", "<ul>\n<li>foo\n<ul>\n<li>bar\n<ul>\n<li>baz\n<ul>\n<li>boo</li>\n</ul>\n</li>\n</ul>\n</li>\n</ul>\n</li>\n</ul>", "", context: "Example 294\nSection Container blocks / List items\n");
        }

        // One is not enough:
        [Test]
        public void ContainerBlocksListItems_Example295()
        {
            // Example 295
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - foo
            //      - bar
            //       - baz
            //        - boo
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     <li>bar</li>
            //     <li>baz</li>
            //     <li>boo</li>
            //     </ul>

            TestParser.TestSpec("- foo\n - bar\n  - baz\n   - boo", "<ul>\n<li>foo</li>\n<li>bar</li>\n<li>baz</li>\n<li>boo</li>\n</ul>", "", context: "Example 295\nSection Container blocks / List items\n");
        }

        // Here we need four, because the list marker is wider:
        [Test]
        public void ContainerBlocksListItems_Example296()
        {
            // Example 296
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     10) foo
            //         - bar
            //
            // Should be rendered as:
            //     <ol start="10">
            //     <li>foo
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            //     </li>
            //     </ol>

            TestParser.TestSpec("10) foo\n    - bar", "<ol start=\"10\">\n<li>foo\n<ul>\n<li>bar</li>\n</ul>\n</li>\n</ol>", "", context: "Example 296\nSection Container blocks / List items\n");
        }

        // Three is not enough:
        [Test]
        public void ContainerBlocksListItems_Example297()
        {
            // Example 297
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     10) foo
            //        - bar
            //
            // Should be rendered as:
            //     <ol start="10">
            //     <li>foo</li>
            //     </ol>
            //     <ul>
            //     <li>bar</li>
            //     </ul>

            TestParser.TestSpec("10) foo\n   - bar", "<ol start=\"10\">\n<li>foo</li>\n</ol>\n<ul>\n<li>bar</li>\n</ul>", "", context: "Example 297\nSection Container blocks / List items\n");
        }

        // A list may be the first block in a list item:
        [Test]
        public void ContainerBlocksListItems_Example298()
        {
            // Example 298
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - - foo
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- - foo", "<ul>\n<li>\n<ul>\n<li>foo</li>\n</ul>\n</li>\n</ul>", "", context: "Example 298\nSection Container blocks / List items\n");
        }

        [Test]
        public void ContainerBlocksListItems_Example299()
        {
            // Example 299
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     1. - 2. foo
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <ul>
            //     <li>
            //     <ol start="2">
            //     <li>foo</li>
            //     </ol>
            //     </li>
            //     </ul>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1. - 2. foo", "<ol>\n<li>\n<ul>\n<li>\n<ol start=\"2\">\n<li>foo</li>\n</ol>\n</li>\n</ul>\n</li>\n</ol>", "", context: "Example 299\nSection Container blocks / List items\n");
        }

        // A list item can contain a heading:
        [Test]
        public void ContainerBlocksListItems_Example300()
        {
            // Example 300
            // Section: Container blocks / List items
            //
            // The following Markdown:
            //     - # Foo
            //     - Bar
            //       ---
            //       baz
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <h1>Foo</h1>
            //     </li>
            //     <li>
            //     <h2>Bar</h2>
            //     baz</li>
            //     </ul>

            TestParser.TestSpec("- # Foo\n- Bar\n  ---\n  baz", "<ul>\n<li>\n<h1>Foo</h1>\n</li>\n<li>\n<h2>Bar</h2>\nbaz</li>\n</ul>", "", context: "Example 300\nSection Container blocks / List items\n");
        }
    }

    [TestFixture]
    public class TestContainerBlocksLists
    {
        // ### Motivation
        // 
        // John Gruber's Markdown spec says the following about list items:
        // 
        // 1. "List markers typically start at the left margin, but may be indented
        //    by up to three spaces. List markers must be followed by one or more
        //    spaces or a tab."
        // 
        // 2. "To make lists look nice, you can wrap items with hanging indents....
        //    But if you don't want to, you don't have to."
        // 
        // 3. "List items may consist of multiple paragraphs. Each subsequent
        //    paragraph in a list item must be indented by either 4 spaces or one
        //    tab."
        // 
        // 4. "It looks nice if you indent every line of the subsequent paragraphs,
        //    but here again, Markdown will allow you to be lazy."
        // 
        // 5. "To put a blockquote within a list item, the blockquote's `>`
        //    delimiters need to be indented."
        // 
        // 6. "To put a code block within a list item, the code block needs to be
        //    indented twice — 8 spaces or two tabs."
        // 
        // These rules specify that a paragraph under a list item must be indented
        // four spaces (presumably, from the left margin, rather than the start of
        // the list marker, but this is not said), and that code under a list item
        // must be indented eight spaces instead of the usual four.  They also say
        // that a block quote must be indented, but not by how much; however, the
        // example given has four spaces indentation.  Although nothing is said
        // about other kinds of block-level content, it is certainly reasonable to
        // infer that *all* block elements under a list item, including other
        // lists, must be indented four spaces.  This principle has been called the
        // *four-space rule*.
        // 
        // The four-space rule is clear and principled, and if the reference
        // implementation `Markdown.pl` had followed it, it probably would have
        // become the standard.  However, `Markdown.pl` allowed paragraphs and
        // sublists to start with only two spaces indentation, at least on the
        // outer level.  Worse, its behavior was inconsistent: a sublist of an
        // outer-level list needed two spaces indentation, but a sublist of this
        // sublist needed three spaces.  It is not surprising, then, that different
        // implementations of Markdown have developed very different rules for
        // determining what comes under a list item.  (Pandoc and python-Markdown,
        // for example, stuck with Gruber's syntax description and the four-space
        // rule, while discount, redcarpet, marked, PHP Markdown, and others
        // followed `Markdown.pl`'s behavior more closely.)
        // 
        // Unfortunately, given the divergences between implementations, there
        // is no way to give a spec for list items that will be guaranteed not
        // to break any existing documents.  However, the spec given here should
        // correctly handle lists formatted with either the four-space rule or
        // the more forgiving `Markdown.pl` behavior, provided they are laid out
        // in a way that is natural for a human to read.
        // 
        // The strategy here is to let the width and indentation of the list marker
        // determine the indentation necessary for blocks to fall under the list
        // item, rather than having a fixed and arbitrary number.  The writer can
        // think of the body of the list item as a unit which gets indented to the
        // right enough to fit the list marker (and any indentation on the list
        // marker).  (The laziness rule, #5, then allows continuation lines to be
        // unindented if needed.)
        // 
        // This rule is superior, we claim, to any rule requiring a fixed level of
        // indentation from the margin.  The four-space rule is clear but
        // unnatural. It is quite unintuitive that
        // 
        // ``` markdown
        // - foo
        // 
        //   bar
        // 
        //   - baz
        // ```
        // 
        // should be parsed as two lists with an intervening paragraph,
        // 
        // ``` html
        // <ul>
        // <li>foo</li>
        // </ul>
        // <p>bar</p>
        // <ul>
        // <li>baz</li>
        // </ul>
        // ```
        // 
        // as the four-space rule demands, rather than a single list,
        // 
        // ``` html
        // <ul>
        // <li>
        // <p>foo</p>
        // <p>bar</p>
        // <ul>
        // <li>baz</li>
        // </ul>
        // </li>
        // </ul>
        // ```
        // 
        // The choice of four spaces is arbitrary.  It can be learned, but it is
        // not likely to be guessed, and it trips up beginners regularly.
        // 
        // Would it help to adopt a two-space rule?  The problem is that such
        // a rule, together with the rule allowing up to three spaces of indentation for
        // the initial list marker, allows text that is indented *less than* the
        // original list marker to be included in the list item. For example,
        // `Markdown.pl` parses
        // 
        // ``` markdown
        //    - one
        // 
        //   two
        // ```
        // 
        // as a single list item, with `two` a continuation paragraph:
        // 
        // ``` html
        // <ul>
        // <li>
        // <p>one</p>
        // <p>two</p>
        // </li>
        // </ul>
        // ```
        // 
        // and similarly
        // 
        // ``` markdown
        // >   - one
        // >
        // >  two
        // ```
        // 
        // as
        // 
        // ``` html
        // <blockquote>
        // <ul>
        // <li>
        // <p>one</p>
        // <p>two</p>
        // </li>
        // </ul>
        // </blockquote>
        // ```
        // 
        // This is extremely unintuitive.
        // 
        // Rather than requiring a fixed indent from the margin, we could require
        // a fixed indent (say, two spaces, or even one space) from the list marker (which
        // may itself be indented).  This proposal would remove the last anomaly
        // discussed.  Unlike the spec presented above, it would count the following
        // as a list item with a subparagraph, even though the paragraph `bar`
        // is not indented as far as the first paragraph `foo`:
        // 
        // ``` markdown
        //  10. foo
        // 
        //    bar  
        // ```
        // 
        // Arguably this text does read like a list item with `bar` as a subparagraph,
        // which may count in favor of the proposal.  However, on this proposal indented
        // code would have to be indented six spaces after the list marker.  And this
        // would break a lot of existing Markdown, which has the pattern:
        // 
        // ``` markdown
        // 1.  foo
        // 
        //         indented code
        // ```
        // 
        // where the code is indented eight spaces.  The spec above, by contrast, will
        // parse this text as expected, since the code block's indentation is measured
        // from the beginning of `foo`.
        // 
        // The one case that needs special treatment is a list item that *starts*
        // with indented code.  How much indentation is required in that case, since
        // we don't have a "first paragraph" to measure from?  Rule #2 simply stipulates
        // that in such cases, we require one space indentation from the list marker
        // (and then the normal four spaces for the indented code).  This will match the
        // four-space rule in cases where the list marker plus its initial indentation
        // takes four spaces (a common case), but diverge in other cases.
        // 
        // ## Lists
        // 
        // A [list](@) is a sequence of one or more
        // list items [of the same type].  The list items
        // may be separated by any number of blank lines.
        // 
        // Two list items are [of the same type](@)
        // if they begin with a [list marker] of the same type.
        // Two list markers are of the
        // same type if (a) they are bullet list markers using the same character
        // (`-`, `+`, or `*`) or (b) they are ordered list numbers with the same
        // delimiter (either `.` or `)`).
        // 
        // A list is an [ordered list](@)
        // if its constituent list items begin with
        // [ordered list markers], and a
        // [bullet list](@) if its constituent list
        // items begin with [bullet list markers].
        // 
        // The [start number](@)
        // of an [ordered list] is determined by the list number of
        // its initial list item.  The numbers of subsequent list items are
        // disregarded.
        // 
        // A list is [loose](@) if any of its constituent
        // list items are separated by blank lines, or if any of its constituent
        // list items directly contain two block-level elements with a blank line
        // between them.  Otherwise a list is [tight](@).
        // (The difference in HTML output is that paragraphs in a loose list are
        // wrapped in `<p>` tags, while paragraphs in a tight list are not.)
        // 
        // Changing the bullet or ordered list delimiter starts a new list:
        [Test]
        public void ContainerBlocksLists_Example301()
        {
            // Example 301
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - foo
            //     - bar
            //     + baz
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     <li>bar</li>
            //     </ul>
            //     <ul>
            //     <li>baz</li>
            //     </ul>

            TestParser.TestSpec("- foo\n- bar\n+ baz", "<ul>\n<li>foo</li>\n<li>bar</li>\n</ul>\n<ul>\n<li>baz</li>\n</ul>", "", context: "Example 301\nSection Container blocks / Lists\n");
        }

        [Test]
        public void ContainerBlocksLists_Example302()
        {
            // Example 302
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     1. foo
            //     2. bar
            //     3) baz
            //
            // Should be rendered as:
            //     <ol>
            //     <li>foo</li>
            //     <li>bar</li>
            //     </ol>
            //     <ol start="3">
            //     <li>baz</li>
            //     </ol>

            TestParser.TestSpec("1. foo\n2. bar\n3) baz", "<ol>\n<li>foo</li>\n<li>bar</li>\n</ol>\n<ol start=\"3\">\n<li>baz</li>\n</ol>", "", context: "Example 302\nSection Container blocks / Lists\n");
        }

        // In CommonMark, a list can interrupt a paragraph. That is,
        // no blank line is needed to separate a paragraph from a following
        // list:
        [Test]
        public void ContainerBlocksLists_Example303()
        {
            // Example 303
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     Foo
            //     - bar
            //     - baz
            //
            // Should be rendered as:
            //     <p>Foo</p>
            //     <ul>
            //     <li>bar</li>
            //     <li>baz</li>
            //     </ul>

            TestParser.TestSpec("Foo\n- bar\n- baz", "<p>Foo</p>\n<ul>\n<li>bar</li>\n<li>baz</li>\n</ul>", "", context: "Example 303\nSection Container blocks / Lists\n");
        }

        // `Markdown.pl` does not allow this, through fear of triggering a list
        // via a numeral in a hard-wrapped line:
        // 
        // ``` markdown
        // The number of windows in my house is
        // 14.  The number of doors is 6.
        // ```
        // 
        // Oddly, though, `Markdown.pl` *does* allow a blockquote to
        // interrupt a paragraph, even though the same considerations might
        // apply.
        // 
        // In CommonMark, we do allow lists to interrupt paragraphs, for
        // two reasons.  First, it is natural and not uncommon for people
        // to start lists without blank lines:
        // 
        // ``` markdown
        // I need to buy
        // - new shoes
        // - a coat
        // - a plane ticket
        // ```
        // 
        // Second, we are attracted to a
        // 
        // > [principle of uniformity](@):
        // > if a chunk of text has a certain
        // > meaning, it will continue to have the same meaning when put into a
        // > container block (such as a list item or blockquote).
        // 
        // (Indeed, the spec for [list items] and [block quotes] presupposes
        // this principle.) This principle implies that if
        // 
        // ``` markdown
        //   * I need to buy
        //     - new shoes
        //     - a coat
        //     - a plane ticket
        // ```
        // 
        // is a list item containing a paragraph followed by a nested sublist,
        // as all Markdown implementations agree it is (though the paragraph
        // may be rendered without `<p>` tags, since the list is "tight"),
        // then
        // 
        // ``` markdown
        // I need to buy
        // - new shoes
        // - a coat
        // - a plane ticket
        // ```
        // 
        // by itself should be a paragraph followed by a nested sublist.
        // 
        // Since it is well established Markdown practice to allow lists to
        // interrupt paragraphs inside list items, the [principle of
        // uniformity] requires us to allow this outside list items as
        // well.  ([reStructuredText](https://docutils.sourceforge.net/rst.html)
        // takes a different approach, requiring blank lines before lists
        // even inside other list items.)
        // 
        // In order to solve the problem of unwanted lists in paragraphs with
        // hard-wrapped numerals, we allow only lists starting with `1` to
        // interrupt paragraphs.  Thus,
        [Test]
        public void ContainerBlocksLists_Example304()
        {
            // Example 304
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     The number of windows in my house is
            //     14.  The number of doors is 6.
            //
            // Should be rendered as:
            //     <p>The number of windows in my house is
            //     14.  The number of doors is 6.</p>

            TestParser.TestSpec("The number of windows in my house is\n14.  The number of doors is 6.", "<p>The number of windows in my house is\n14.  The number of doors is 6.</p>", "", context: "Example 304\nSection Container blocks / Lists\n");
        }

        // We may still get an unintended result in cases like
        [Test]
        public void ContainerBlocksLists_Example305()
        {
            // Example 305
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     The number of windows in my house is
            //     1.  The number of doors is 6.
            //
            // Should be rendered as:
            //     <p>The number of windows in my house is</p>
            //     <ol>
            //     <li>The number of doors is 6.</li>
            //     </ol>

            TestParser.TestSpec("The number of windows in my house is\n1.  The number of doors is 6.", "<p>The number of windows in my house is</p>\n<ol>\n<li>The number of doors is 6.</li>\n</ol>", "", context: "Example 305\nSection Container blocks / Lists\n");
        }

        // but this rule should prevent most spurious list captures.
        // 
        // There can be any number of blank lines between items:
        [Test]
        public void ContainerBlocksLists_Example306()
        {
            // Example 306
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - foo
            //     
            //     - bar
            //     
            //     
            //     - baz
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     </li>
            //     <li>
            //     <p>bar</p>
            //     </li>
            //     <li>
            //     <p>baz</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- foo\n\n- bar\n\n\n- baz", "<ul>\n<li>\n<p>foo</p>\n</li>\n<li>\n<p>bar</p>\n</li>\n<li>\n<p>baz</p>\n</li>\n</ul>", "", context: "Example 306\nSection Container blocks / Lists\n");
        }

        [Test]
        public void ContainerBlocksLists_Example307()
        {
            // Example 307
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - foo
            //       - bar
            //         - baz
            //     
            //     
            //           bim
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo
            //     <ul>
            //     <li>bar
            //     <ul>
            //     <li>
            //     <p>baz</p>
            //     <p>bim</p>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- foo\n  - bar\n    - baz\n\n\n      bim", "<ul>\n<li>foo\n<ul>\n<li>bar\n<ul>\n<li>\n<p>baz</p>\n<p>bim</p>\n</li>\n</ul>\n</li>\n</ul>\n</li>\n</ul>", "", context: "Example 307\nSection Container blocks / Lists\n");
        }

        // To separate consecutive lists of the same type, or to separate a
        // list from an indented code block that would otherwise be parsed
        // as a subparagraph of the final list item, you can insert a blank HTML
        // comment:
        [Test]
        public void ContainerBlocksLists_Example308()
        {
            // Example 308
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - foo
            //     - bar
            //     
            //     <!-- -->
            //     
            //     - baz
            //     - bim
            //
            // Should be rendered as:
            //     <ul>
            //     <li>foo</li>
            //     <li>bar</li>
            //     </ul>
            //     <!-- -->
            //     <ul>
            //     <li>baz</li>
            //     <li>bim</li>
            //     </ul>

            TestParser.TestSpec("- foo\n- bar\n\n<!-- -->\n\n- baz\n- bim", "<ul>\n<li>foo</li>\n<li>bar</li>\n</ul>\n<!-- -->\n<ul>\n<li>baz</li>\n<li>bim</li>\n</ul>", "", context: "Example 308\nSection Container blocks / Lists\n");
        }

        [Test]
        public void ContainerBlocksLists_Example309()
        {
            // Example 309
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     -   foo
            //     
            //         notcode
            //     
            //     -   foo
            //     
            //     <!-- -->
            //     
            //         code
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>notcode</p>
            //     </li>
            //     <li>
            //     <p>foo</p>
            //     </li>
            //     </ul>
            //     <!-- -->
            //     <pre><code>code
            //     </code></pre>

            TestParser.TestSpec("-   foo\n\n    notcode\n\n-   foo\n\n<!-- -->\n\n    code", "<ul>\n<li>\n<p>foo</p>\n<p>notcode</p>\n</li>\n<li>\n<p>foo</p>\n</li>\n</ul>\n<!-- -->\n<pre><code>code\n</code></pre>", "", context: "Example 309\nSection Container blocks / Lists\n");
        }

        // List items need not be indented to the same level.  The following
        // list items will be treated as items at the same list level,
        // since none is indented enough to belong to the previous list
        // item:
        [Test]
        public void ContainerBlocksLists_Example310()
        {
            // Example 310
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //      - b
            //       - c
            //        - d
            //       - e
            //      - f
            //     - g
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a</li>
            //     <li>b</li>
            //     <li>c</li>
            //     <li>d</li>
            //     <li>e</li>
            //     <li>f</li>
            //     <li>g</li>
            //     </ul>

            TestParser.TestSpec("- a\n - b\n  - c\n   - d\n  - e\n - f\n- g", "<ul>\n<li>a</li>\n<li>b</li>\n<li>c</li>\n<li>d</li>\n<li>e</li>\n<li>f</li>\n<li>g</li>\n</ul>", "", context: "Example 310\nSection Container blocks / Lists\n");
        }

        [Test]
        public void ContainerBlocksLists_Example311()
        {
            // Example 311
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     1. a
            //     
            //       2. b
            //     
            //        3. c
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     </li>
            //     <li>
            //     <p>c</p>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1. a\n\n  2. b\n\n   3. c", "<ol>\n<li>\n<p>a</p>\n</li>\n<li>\n<p>b</p>\n</li>\n<li>\n<p>c</p>\n</li>\n</ol>", "", context: "Example 311\nSection Container blocks / Lists\n");
        }

        // Note, however, that list items may not be preceded by more than
        // three spaces of indentation.  Here `- e` is treated as a paragraph continuation
        // line, because it is indented more than three spaces:
        [Test]
        public void ContainerBlocksLists_Example312()
        {
            // Example 312
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //      - b
            //       - c
            //        - d
            //         - e
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a</li>
            //     <li>b</li>
            //     <li>c</li>
            //     <li>d
            //     - e</li>
            //     </ul>

            TestParser.TestSpec("- a\n - b\n  - c\n   - d\n    - e", "<ul>\n<li>a</li>\n<li>b</li>\n<li>c</li>\n<li>d\n- e</li>\n</ul>", "", context: "Example 312\nSection Container blocks / Lists\n");
        }

        // And here, `3. c` is treated as in indented code block,
        // because it is indented four spaces and preceded by a
        // blank line.
        [Test]
        public void ContainerBlocksLists_Example313()
        {
            // Example 313
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     1. a
            //     
            //       2. b
            //     
            //         3. c
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     </li>
            //     </ol>
            //     <pre><code>3. c
            //     </code></pre>

            TestParser.TestSpec("1. a\n\n  2. b\n\n    3. c", "<ol>\n<li>\n<p>a</p>\n</li>\n<li>\n<p>b</p>\n</li>\n</ol>\n<pre><code>3. c\n</code></pre>", "", context: "Example 313\nSection Container blocks / Lists\n");
        }

        // This is a loose list, because there is a blank line between
        // two of the list items:
        [Test]
        public void ContainerBlocksLists_Example314()
        {
            // Example 314
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //     - b
            //     
            //     - c
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     </li>
            //     <li>
            //     <p>c</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- a\n- b\n\n- c", "<ul>\n<li>\n<p>a</p>\n</li>\n<li>\n<p>b</p>\n</li>\n<li>\n<p>c</p>\n</li>\n</ul>", "", context: "Example 314\nSection Container blocks / Lists\n");
        }

        // So is this, with a empty second item:
        [Test]
        public void ContainerBlocksLists_Example315()
        {
            // Example 315
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     * a
            //     *
            //     
            //     * c
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li></li>
            //     <li>
            //     <p>c</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("* a\n*\n\n* c", "<ul>\n<li>\n<p>a</p>\n</li>\n<li></li>\n<li>\n<p>c</p>\n</li>\n</ul>", "", context: "Example 315\nSection Container blocks / Lists\n");
        }

        // These are loose lists, even though there are no blank lines between the items,
        // because one of the items directly contains two block-level elements
        // with a blank line between them:
        [Test]
        public void ContainerBlocksLists_Example316()
        {
            // Example 316
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //     - b
            //     
            //       c
            //     - d
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     <p>c</p>
            //     </li>
            //     <li>
            //     <p>d</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- a\n- b\n\n  c\n- d", "<ul>\n<li>\n<p>a</p>\n</li>\n<li>\n<p>b</p>\n<p>c</p>\n</li>\n<li>\n<p>d</p>\n</li>\n</ul>", "", context: "Example 316\nSection Container blocks / Lists\n");
        }

        [Test]
        public void ContainerBlocksLists_Example317()
        {
            // Example 317
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //     - b
            //     
            //       [ref]: /url
            //     - d
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     </li>
            //     <li>
            //     <p>d</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- a\n- b\n\n  [ref]: /url\n- d", "<ul>\n<li>\n<p>a</p>\n</li>\n<li>\n<p>b</p>\n</li>\n<li>\n<p>d</p>\n</li>\n</ul>", "", context: "Example 317\nSection Container blocks / Lists\n");
        }

        // This is a tight list, because the blank lines are in a code block:
        [Test]
        public void ContainerBlocksLists_Example318()
        {
            // Example 318
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //     - ```
            //       b
            //     
            //     
            //       ```
            //     - c
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a</li>
            //     <li>
            //     <pre><code>b
            //     
            //     
            //     </code></pre>
            //     </li>
            //     <li>c</li>
            //     </ul>

            TestParser.TestSpec("- a\n- ```\n  b\n\n\n  ```\n- c", "<ul>\n<li>a</li>\n<li>\n<pre><code>b\n\n\n</code></pre>\n</li>\n<li>c</li>\n</ul>", "", context: "Example 318\nSection Container blocks / Lists\n");
        }

        // This is a tight list, because the blank line is between two
        // paragraphs of a sublist.  So the sublist is loose while
        // the outer list is tight:
        [Test]
        public void ContainerBlocksLists_Example319()
        {
            // Example 319
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //       - b
            //     
            //         c
            //     - d
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a
            //     <ul>
            //     <li>
            //     <p>b</p>
            //     <p>c</p>
            //     </li>
            //     </ul>
            //     </li>
            //     <li>d</li>
            //     </ul>

            TestParser.TestSpec("- a\n  - b\n\n    c\n- d", "<ul>\n<li>a\n<ul>\n<li>\n<p>b</p>\n<p>c</p>\n</li>\n</ul>\n</li>\n<li>d</li>\n</ul>", "", context: "Example 319\nSection Container blocks / Lists\n");
        }

        // This is a tight list, because the blank line is inside the
        // block quote:
        [Test]
        public void ContainerBlocksLists_Example320()
        {
            // Example 320
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     * a
            //       > b
            //       >
            //     * c
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a
            //     <blockquote>
            //     <p>b</p>
            //     </blockquote>
            //     </li>
            //     <li>c</li>
            //     </ul>

            TestParser.TestSpec("* a\n  > b\n  >\n* c", "<ul>\n<li>a\n<blockquote>\n<p>b</p>\n</blockquote>\n</li>\n<li>c</li>\n</ul>", "", context: "Example 320\nSection Container blocks / Lists\n");
        }

        // This list is tight, because the consecutive block elements
        // are not separated by blank lines:
        [Test]
        public void ContainerBlocksLists_Example321()
        {
            // Example 321
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //       > b
            //       ```
            //       c
            //       ```
            //     - d
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a
            //     <blockquote>
            //     <p>b</p>
            //     </blockquote>
            //     <pre><code>c
            //     </code></pre>
            //     </li>
            //     <li>d</li>
            //     </ul>

            TestParser.TestSpec("- a\n  > b\n  ```\n  c\n  ```\n- d", "<ul>\n<li>a\n<blockquote>\n<p>b</p>\n</blockquote>\n<pre><code>c\n</code></pre>\n</li>\n<li>d</li>\n</ul>", "", context: "Example 321\nSection Container blocks / Lists\n");
        }

        // A single-paragraph list is tight:
        [Test]
        public void ContainerBlocksLists_Example322()
        {
            // Example 322
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a</li>
            //     </ul>

            TestParser.TestSpec("- a", "<ul>\n<li>a</li>\n</ul>", "", context: "Example 322\nSection Container blocks / Lists\n");
        }

        [Test]
        public void ContainerBlocksLists_Example323()
        {
            // Example 323
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //       - b
            //
            // Should be rendered as:
            //     <ul>
            //     <li>a
            //     <ul>
            //     <li>b</li>
            //     </ul>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- a\n  - b", "<ul>\n<li>a\n<ul>\n<li>b</li>\n</ul>\n</li>\n</ul>", "", context: "Example 323\nSection Container blocks / Lists\n");
        }

        // This list is loose, because of the blank line between the
        // two block elements in the list item:
        [Test]
        public void ContainerBlocksLists_Example324()
        {
            // Example 324
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     1. ```
            //        foo
            //        ```
            //     
            //        bar
            //
            // Should be rendered as:
            //     <ol>
            //     <li>
            //     <pre><code>foo
            //     </code></pre>
            //     <p>bar</p>
            //     </li>
            //     </ol>

            TestParser.TestSpec("1. ```\n   foo\n   ```\n\n   bar", "<ol>\n<li>\n<pre><code>foo\n</code></pre>\n<p>bar</p>\n</li>\n</ol>", "", context: "Example 324\nSection Container blocks / Lists\n");
        }

        // Here the outer list is loose, the inner list tight:
        [Test]
        public void ContainerBlocksLists_Example325()
        {
            // Example 325
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     * foo
            //       * bar
            //     
            //       baz
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            //     <p>baz</p>
            //     </li>
            //     </ul>

            TestParser.TestSpec("* foo\n  * bar\n\n  baz", "<ul>\n<li>\n<p>foo</p>\n<ul>\n<li>bar</li>\n</ul>\n<p>baz</p>\n</li>\n</ul>", "", context: "Example 325\nSection Container blocks / Lists\n");
        }

        [Test]
        public void ContainerBlocksLists_Example326()
        {
            // Example 326
            // Section: Container blocks / Lists
            //
            // The following Markdown:
            //     - a
            //       - b
            //       - c
            //     
            //     - d
            //       - e
            //       - f
            //
            // Should be rendered as:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     <ul>
            //     <li>b</li>
            //     <li>c</li>
            //     </ul>
            //     </li>
            //     <li>
            //     <p>d</p>
            //     <ul>
            //     <li>e</li>
            //     <li>f</li>
            //     </ul>
            //     </li>
            //     </ul>

            TestParser.TestSpec("- a\n  - b\n  - c\n\n- d\n  - e\n  - f", "<ul>\n<li>\n<p>a</p>\n<ul>\n<li>b</li>\n<li>c</li>\n</ul>\n</li>\n<li>\n<p>d</p>\n<ul>\n<li>e</li>\n<li>f</li>\n</ul>\n</li>\n</ul>", "", context: "Example 326\nSection Container blocks / Lists\n");
        }
    }

    [TestFixture]
    public class TestInlines
    {
        // # Inlines
        // 
        // Inlines are parsed sequentially from the beginning of the character
        // stream to the end (left to right, in left-to-right languages).
        // Thus, for example, in
        [Test]
        public void Inlines_Example327()
        {
            // Example 327
            // Section: Inlines
            //
            // The following Markdown:
            //     `hi`lo`
            //
            // Should be rendered as:
            //     <p><code>hi</code>lo`</p>

            TestParser.TestSpec("`hi`lo`", "<p><code>hi</code>lo`</p>", "", context: "Example 327\nSection Inlines\n");
        }
    }

    [TestFixture]
    public class TestInlinesCodeSpans
    {
        // `hi` is parsed as code, leaving the backtick at the end as a literal
        // backtick.
        // 
        // 
        // 
        // ## Code spans
        // 
        // A [backtick string](@)
        // is a string of one or more backtick characters (`` ` ``) that is neither
        // preceded nor followed by a backtick.
        // 
        // A [code span](@) begins with a backtick string and ends with
        // a backtick string of equal length.  The contents of the code span are
        // the characters between these two backtick strings, normalized in the
        // following ways:
        // 
        // - First, [line endings] are converted to [spaces].
        // - If the resulting string both begins *and* ends with a [space]
        //   character, but does not consist entirely of [space]
        //   characters, a single [space] character is removed from the
        //   front and back.  This allows you to include code that begins
        //   or ends with backtick characters, which must be separated by
        //   whitespace from the opening or closing backtick strings.
        // 
        // This is a simple code span:
        [Test]
        public void InlinesCodeSpans_Example328()
        {
            // Example 328
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `foo`
            //
            // Should be rendered as:
            //     <p><code>foo</code></p>

            TestParser.TestSpec("`foo`", "<p><code>foo</code></p>", "", context: "Example 328\nSection Inlines / Code spans\n");
        }

        // Here two backticks are used, because the code contains a backtick.
        // This example also illustrates stripping of a single leading and
        // trailing space:
        [Test]
        public void InlinesCodeSpans_Example329()
        {
            // Example 329
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `` foo ` bar ``
            //
            // Should be rendered as:
            //     <p><code>foo ` bar</code></p>

            TestParser.TestSpec("`` foo ` bar ``", "<p><code>foo ` bar</code></p>", "", context: "Example 329\nSection Inlines / Code spans\n");
        }

        // This example shows the motivation for stripping leading and trailing
        // spaces:
        [Test]
        public void InlinesCodeSpans_Example330()
        {
            // Example 330
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ` `` `
            //
            // Should be rendered as:
            //     <p><code>``</code></p>

            TestParser.TestSpec("` `` `", "<p><code>``</code></p>", "", context: "Example 330\nSection Inlines / Code spans\n");
        }

        // Note that only *one* space is stripped:
        [Test]
        public void InlinesCodeSpans_Example331()
        {
            // Example 331
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `  ``  `
            //
            // Should be rendered as:
            //     <p><code> `` </code></p>

            TestParser.TestSpec("`  ``  `", "<p><code> `` </code></p>", "", context: "Example 331\nSection Inlines / Code spans\n");
        }

        // The stripping only happens if the space is on both
        // sides of the string:
        [Test]
        public void InlinesCodeSpans_Example332()
        {
            // Example 332
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ` a`
            //
            // Should be rendered as:
            //     <p><code> a</code></p>

            TestParser.TestSpec("` a`", "<p><code> a</code></p>", "", context: "Example 332\nSection Inlines / Code spans\n");
        }

        // Only [spaces], and not [unicode whitespace] in general, are
        // stripped in this way:
        [Test]
        public void InlinesCodeSpans_Example333()
        {
            // Example 333
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ` b `
            //
            // Should be rendered as:
            //     <p><code> b </code></p>

            TestParser.TestSpec("` b `", "<p><code> b </code></p>", "", context: "Example 333\nSection Inlines / Code spans\n");
        }

        // No stripping occurs if the code span contains only spaces:
        [Test]
        public void InlinesCodeSpans_Example334()
        {
            // Example 334
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ` `
            //     `  `
            //
            // Should be rendered as:
            //     <p><code> </code>
            //     <code>  </code></p>

            TestParser.TestSpec("` `\n`  `", "<p><code> </code>\n<code>  </code></p>", "", context: "Example 334\nSection Inlines / Code spans\n");
        }

        // [Line endings] are treated like spaces:
        [Test]
        public void InlinesCodeSpans_Example335()
        {
            // Example 335
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ``
            //     foo
            //     bar  
            //     baz
            //     ``
            //
            // Should be rendered as:
            //     <p><code>foo bar   baz</code></p>

            TestParser.TestSpec("``\nfoo\nbar  \nbaz\n``", "<p><code>foo bar   baz</code></p>", "", context: "Example 335\nSection Inlines / Code spans\n");
        }

        [Test]
        public void InlinesCodeSpans_Example336()
        {
            // Example 336
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ``
            //     foo 
            //     ``
            //
            // Should be rendered as:
            //     <p><code>foo </code></p>

            TestParser.TestSpec("``\nfoo \n``", "<p><code>foo </code></p>", "", context: "Example 336\nSection Inlines / Code spans\n");
        }

        // Interior spaces are not collapsed:
        [Test]
        public void InlinesCodeSpans_Example337()
        {
            // Example 337
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `foo   bar 
            //     baz`
            //
            // Should be rendered as:
            //     <p><code>foo   bar  baz</code></p>

            TestParser.TestSpec("`foo   bar \nbaz`", "<p><code>foo   bar  baz</code></p>", "", context: "Example 337\nSection Inlines / Code spans\n");
        }

        // Note that browsers will typically collapse consecutive spaces
        // when rendering `<code>` elements, so it is recommended that
        // the following CSS be used:
        // 
        //     code{white-space: pre-wrap;}
        // 
        // 
        // Note that backslash escapes do not work in code spans. All backslashes
        // are treated literally:
        [Test]
        public void InlinesCodeSpans_Example338()
        {
            // Example 338
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `foo\`bar`
            //
            // Should be rendered as:
            //     <p><code>foo\</code>bar`</p>

            TestParser.TestSpec("`foo\\`bar`", "<p><code>foo\\</code>bar`</p>", "", context: "Example 338\nSection Inlines / Code spans\n");
        }

        // Backslash escapes are never needed, because one can always choose a
        // string of *n* backtick characters as delimiters, where the code does
        // not contain any strings of exactly *n* backtick characters.
        [Test]
        public void InlinesCodeSpans_Example339()
        {
            // Example 339
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ``foo`bar``
            //
            // Should be rendered as:
            //     <p><code>foo`bar</code></p>

            TestParser.TestSpec("``foo`bar``", "<p><code>foo`bar</code></p>", "", context: "Example 339\nSection Inlines / Code spans\n");
        }

        [Test]
        public void InlinesCodeSpans_Example340()
        {
            // Example 340
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ` foo `` bar `
            //
            // Should be rendered as:
            //     <p><code>foo `` bar</code></p>

            TestParser.TestSpec("` foo `` bar `", "<p><code>foo `` bar</code></p>", "", context: "Example 340\nSection Inlines / Code spans\n");
        }

        // Code span backticks have higher precedence than any other inline
        // constructs except HTML tags and autolinks.  Thus, for example, this is
        // not parsed as emphasized text, since the second `*` is part of a code
        // span:
        [Test]
        public void InlinesCodeSpans_Example341()
        {
            // Example 341
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     *foo`*`
            //
            // Should be rendered as:
            //     <p>*foo<code>*</code></p>

            TestParser.TestSpec("*foo`*`", "<p>*foo<code>*</code></p>", "", context: "Example 341\nSection Inlines / Code spans\n");
        }

        // And this is not parsed as a link:
        [Test]
        public void InlinesCodeSpans_Example342()
        {
            // Example 342
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     [not a `link](/foo`)
            //
            // Should be rendered as:
            //     <p>[not a <code>link](/foo</code>)</p>

            TestParser.TestSpec("[not a `link](/foo`)", "<p>[not a <code>link](/foo</code>)</p>", "", context: "Example 342\nSection Inlines / Code spans\n");
        }

        // Code spans, HTML tags, and autolinks have the same precedence.
        // Thus, this is code:
        [Test]
        public void InlinesCodeSpans_Example343()
        {
            // Example 343
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `<a href="`">`
            //
            // Should be rendered as:
            //     <p><code>&lt;a href=&quot;</code>&quot;&gt;`</p>

            TestParser.TestSpec("`<a href=\"`\">`", "<p><code>&lt;a href=&quot;</code>&quot;&gt;`</p>", "", context: "Example 343\nSection Inlines / Code spans\n");
        }

        // But this is an HTML tag:
        [Test]
        public void InlinesCodeSpans_Example344()
        {
            // Example 344
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     <a href="`">`
            //
            // Should be rendered as:
            //     <p><a href="`">`</p>

            TestParser.TestSpec("<a href=\"`\">`", "<p><a href=\"`\">`</p>", "", context: "Example 344\nSection Inlines / Code spans\n");
        }

        // And this is code:
        [Test]
        public void InlinesCodeSpans_Example345()
        {
            // Example 345
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `<https://foo.bar.`baz>`
            //
            // Should be rendered as:
            //     <p><code>&lt;https://foo.bar.</code>baz&gt;`</p>

            TestParser.TestSpec("`<https://foo.bar.`baz>`", "<p><code>&lt;https://foo.bar.</code>baz&gt;`</p>", "", context: "Example 345\nSection Inlines / Code spans\n");
        }

        // But this is an autolink:
        [Test]
        public void InlinesCodeSpans_Example346()
        {
            // Example 346
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     <https://foo.bar.`baz>`
            //
            // Should be rendered as:
            //     <p><a href="https://foo.bar.%60baz">https://foo.bar.`baz</a>`</p>

            TestParser.TestSpec("<https://foo.bar.`baz>`", "<p><a href=\"https://foo.bar.%60baz\">https://foo.bar.`baz</a>`</p>", "", context: "Example 346\nSection Inlines / Code spans\n");
        }

        // When a backtick string is not closed by a matching backtick string,
        // we just have literal backticks:
        [Test]
        public void InlinesCodeSpans_Example347()
        {
            // Example 347
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     ```foo``
            //
            // Should be rendered as:
            //     <p>```foo``</p>

            TestParser.TestSpec("```foo``", "<p>```foo``</p>", "", context: "Example 347\nSection Inlines / Code spans\n");
        }

        [Test]
        public void InlinesCodeSpans_Example348()
        {
            // Example 348
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `foo
            //
            // Should be rendered as:
            //     <p>`foo</p>

            TestParser.TestSpec("`foo", "<p>`foo</p>", "", context: "Example 348\nSection Inlines / Code spans\n");
        }

        // The following case also illustrates the need for opening and
        // closing backtick strings to be equal in length:
        [Test]
        public void InlinesCodeSpans_Example349()
        {
            // Example 349
            // Section: Inlines / Code spans
            //
            // The following Markdown:
            //     `foo``bar``
            //
            // Should be rendered as:
            //     <p>`foo<code>bar</code></p>

            TestParser.TestSpec("`foo``bar``", "<p>`foo<code>bar</code></p>", "", context: "Example 349\nSection Inlines / Code spans\n");
        }
    }

    [TestFixture]
    public class TestInlinesEmphasisAndStrongEmphasis
    {
        // ## Emphasis and strong emphasis
        // 
        // John Gruber's original [Markdown syntax
        // description](https://daringfireball.net/projects/markdown/syntax#em) says:
        // 
        // > Markdown treats asterisks (`*`) and underscores (`_`) as indicators of
        // > emphasis. Text wrapped with one `*` or `_` will be wrapped with an HTML
        // > `<em>` tag; double `*`'s or `_`'s will be wrapped with an HTML `<strong>`
        // > tag.
        // 
        // This is enough for most users, but these rules leave much undecided,
        // especially when it comes to nested emphasis.  The original
        // `Markdown.pl` test suite makes it clear that triple `***` and
        // `___` delimiters can be used for strong emphasis, and most
        // implementations have also allowed the following patterns:
        // 
        // ``` markdown
        // ***strong emph***
        // ***strong** in emph*
        // ***emph* in strong**
        // **in strong *emph***
        // *in emph **strong***
        // ```
        // 
        // The following patterns are less widely supported, but the intent
        // is clear and they are useful (especially in contexts like bibliography
        // entries):
        // 
        // ``` markdown
        // *emph *with emph* in it*
        // **strong **with strong** in it**
        // ```
        // 
        // Many implementations have also restricted intraword emphasis to
        // the `*` forms, to avoid unwanted emphasis in words containing
        // internal underscores.  (It is best practice to put these in code
        // spans, but users often do not.)
        // 
        // ``` markdown
        // internal emphasis: foo*bar*baz
        // no emphasis: foo_bar_baz
        // ```
        // 
        // The rules given below capture all of these patterns, while allowing
        // for efficient parsing strategies that do not backtrack.
        // 
        // First, some definitions.  A [delimiter run](@) is either
        // a sequence of one or more `*` characters that is not preceded or
        // followed by a non-backslash-escaped `*` character, or a sequence
        // of one or more `_` characters that is not preceded or followed by
        // a non-backslash-escaped `_` character.
        // 
        // A [left-flanking delimiter run](@) is
        // a [delimiter run] that is (1) not followed by [Unicode whitespace],
        // and either (2a) not followed by a [Unicode punctuation character], or
        // (2b) followed by a [Unicode punctuation character] and
        // preceded by [Unicode whitespace] or a [Unicode punctuation character].
        // For purposes of this definition, the beginning and the end of
        // the line count as Unicode whitespace.
        // 
        // A [right-flanking delimiter run](@) is
        // a [delimiter run] that is (1) not preceded by [Unicode whitespace],
        // and either (2a) not preceded by a [Unicode punctuation character], or
        // (2b) preceded by a [Unicode punctuation character] and
        // followed by [Unicode whitespace] or a [Unicode punctuation character].
        // For purposes of this definition, the beginning and the end of
        // the line count as Unicode whitespace.
        // 
        // Here are some examples of delimiter runs.
        // 
        //   - left-flanking but not right-flanking:
        // 
        //     ```
        //     ***abc
        //       _abc
        //     **"abc"
        //      _"abc"
        //     ```
        // 
        //   - right-flanking but not left-flanking:
        // 
        //     ```
        //      abc***
        //      abc_
        //     "abc"**
        //     "abc"_
        //     ```
        // 
        //   - Both left and right-flanking:
        // 
        //     ```
        //      abc***def
        //     "abc"_"def"
        //     ```
        // 
        //   - Neither left nor right-flanking:
        // 
        //     ```
        //     abc *** def
        //     a _ b
        //     ```
        // 
        // (The idea of distinguishing left-flanking and right-flanking
        // delimiter runs based on the character before and the character
        // after comes from Roopesh Chander's
        // [vfmd](https://web.archive.org/web/20220608143320/http://www.vfmd.org/vfmd-spec/specification/#procedure-for-identifying-emphasis-tags).
        // vfmd uses the terminology "emphasis indicator string" instead of "delimiter
        // run," and its rules for distinguishing left- and right-flanking runs
        // are a bit more complex than the ones given here.)
        // 
        // The following rules define emphasis and strong emphasis:
        // 
        // 1.  A single `*` character [can open emphasis](@)
        //     iff (if and only if) it is part of a [left-flanking delimiter run].
        // 
        // 2.  A single `_` character [can open emphasis] iff
        //     it is part of a [left-flanking delimiter run]
        //     and either (a) not part of a [right-flanking delimiter run]
        //     or (b) part of a [right-flanking delimiter run]
        //     preceded by a [Unicode punctuation character].
        // 
        // 3.  A single `*` character [can close emphasis](@)
        //     iff it is part of a [right-flanking delimiter run].
        // 
        // 4.  A single `_` character [can close emphasis] iff
        //     it is part of a [right-flanking delimiter run]
        //     and either (a) not part of a [left-flanking delimiter run]
        //     or (b) part of a [left-flanking delimiter run]
        //     followed by a [Unicode punctuation character].
        // 
        // 5.  A double `**` [can open strong emphasis](@)
        //     iff it is part of a [left-flanking delimiter run].
        // 
        // 6.  A double `__` [can open strong emphasis] iff
        //     it is part of a [left-flanking delimiter run]
        //     and either (a) not part of a [right-flanking delimiter run]
        //     or (b) part of a [right-flanking delimiter run]
        //     preceded by a [Unicode punctuation character].
        // 
        // 7.  A double `**` [can close strong emphasis](@)
        //     iff it is part of a [right-flanking delimiter run].
        // 
        // 8.  A double `__` [can close strong emphasis] iff
        //     it is part of a [right-flanking delimiter run]
        //     and either (a) not part of a [left-flanking delimiter run]
        //     or (b) part of a [left-flanking delimiter run]
        //     followed by a [Unicode punctuation character].
        // 
        // 9.  Emphasis begins with a delimiter that [can open emphasis] and ends
        //     with a delimiter that [can close emphasis], and that uses the same
        //     character (`_` or `*`) as the opening delimiter.  The
        //     opening and closing delimiters must belong to separate
        //     [delimiter runs].  If one of the delimiters can both
        //     open and close emphasis, then the sum of the lengths of the
        //     delimiter runs containing the opening and closing delimiters
        //     must not be a multiple of 3 unless both lengths are
        //     multiples of 3.
        // 
        // 10. Strong emphasis begins with a delimiter that
        //     [can open strong emphasis] and ends with a delimiter that
        //     [can close strong emphasis], and that uses the same character
        //     (`_` or `*`) as the opening delimiter.  The
        //     opening and closing delimiters must belong to separate
        //     [delimiter runs].  If one of the delimiters can both open
        //     and close strong emphasis, then the sum of the lengths of
        //     the delimiter runs containing the opening and closing
        //     delimiters must not be a multiple of 3 unless both lengths
        //     are multiples of 3.
        // 
        // 11. A literal `*` character cannot occur at the beginning or end of
        //     `*`-delimited emphasis or `**`-delimited strong emphasis, unless it
        //     is backslash-escaped.
        // 
        // 12. A literal `_` character cannot occur at the beginning or end of
        //     `_`-delimited emphasis or `__`-delimited strong emphasis, unless it
        //     is backslash-escaped.
        // 
        // Where rules 1--12 above are compatible with multiple parsings,
        // the following principles resolve ambiguity:
        // 
        // 13. The number of nestings should be minimized. Thus, for example,
        //     an interpretation `<strong>...</strong>` is always preferred to
        //     `<em><em>...</em></em>`.
        // 
        // 14. An interpretation `<em><strong>...</strong></em>` is always
        //     preferred to `<strong><em>...</em></strong>`.
        // 
        // 15. When two potential emphasis or strong emphasis spans overlap,
        //     so that the second begins before the first ends and ends after
        //     the first ends, the first takes precedence. Thus, for example,
        //     `*foo _bar* baz_` is parsed as `<em>foo _bar</em> baz_` rather
        //     than `*foo <em>bar* baz</em>`.
        // 
        // 16. When there are two potential emphasis or strong emphasis spans
        //     with the same closing delimiter, the shorter one (the one that
        //     opens later) takes precedence. Thus, for example,
        //     `**foo **bar baz**` is parsed as `**foo <strong>bar baz</strong>`
        //     rather than `<strong>foo **bar baz</strong>`.
        // 
        // 17. Inline code spans, links, images, and HTML tags group more tightly
        //     than emphasis.  So, when there is a choice between an interpretation
        //     that contains one of these elements and one that does not, the
        //     former always wins.  Thus, for example, `*[foo*](bar)` is
        //     parsed as `*<a href="bar">foo*</a>` rather than as
        //     `<em>[foo</em>](bar)`.
        // 
        // These rules can be illustrated through a series of examples.
        // 
        // Rule 1:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example350()
        {
            // Example 350
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo bar*
            //
            // Should be rendered as:
            //     <p><em>foo bar</em></p>

            TestParser.TestSpec("*foo bar*", "<p><em>foo bar</em></p>", "", context: "Example 350\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not emphasis, because the opening `*` is followed by
        // whitespace, and hence not part of a [left-flanking delimiter run]:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example351()
        {
            // Example 351
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     a * foo bar*
            //
            // Should be rendered as:
            //     <p>a * foo bar*</p>

            TestParser.TestSpec("a * foo bar*", "<p>a * foo bar*</p>", "", context: "Example 351\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not emphasis, because the opening `*` is preceded
        // by an alphanumeric and followed by punctuation, and hence
        // not part of a [left-flanking delimiter run]:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example352()
        {
            // Example 352
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     a*"foo"*
            //
            // Should be rendered as:
            //     <p>a*&quot;foo&quot;*</p>

            TestParser.TestSpec("a*\"foo\"*", "<p>a*&quot;foo&quot;*</p>", "", context: "Example 352\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Unicode nonbreaking spaces count as whitespace, too:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example353()
        {
            // Example 353
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     * a *
            //
            // Should be rendered as:
            //     <p>* a *</p>

            TestParser.TestSpec("* a *", "<p>* a *</p>", "", context: "Example 353\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Unicode symbols count as punctuation, too:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example354()
        {
            // Example 354
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *$*alpha.
            //     
            //     *£*bravo.
            //     
            //     *€*charlie.
            //
            // Should be rendered as:
            //     <p>*$*alpha.</p>
            //     <p>*£*bravo.</p>
            //     <p>*€*charlie.</p>

            TestParser.TestSpec("*$*alpha.\n\n*£*bravo.\n\n*€*charlie.", "<p>*$*alpha.</p>\n<p>*£*bravo.</p>\n<p>*€*charlie.</p>", "", context: "Example 354\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Intraword emphasis with `*` is permitted:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example355()
        {
            // Example 355
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo*bar*
            //
            // Should be rendered as:
            //     <p>foo<em>bar</em></p>

            TestParser.TestSpec("foo*bar*", "<p>foo<em>bar</em></p>", "", context: "Example 355\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example356()
        {
            // Example 356
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     5*6*78
            //
            // Should be rendered as:
            //     <p>5<em>6</em>78</p>

            TestParser.TestSpec("5*6*78", "<p>5<em>6</em>78</p>", "", context: "Example 356\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 2:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example357()
        {
            // Example 357
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo bar_
            //
            // Should be rendered as:
            //     <p><em>foo bar</em></p>

            TestParser.TestSpec("_foo bar_", "<p><em>foo bar</em></p>", "", context: "Example 357\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not emphasis, because the opening `_` is followed by
        // whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example358()
        {
            // Example 358
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _ foo bar_
            //
            // Should be rendered as:
            //     <p>_ foo bar_</p>

            TestParser.TestSpec("_ foo bar_", "<p>_ foo bar_</p>", "", context: "Example 358\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not emphasis, because the opening `_` is preceded
        // by an alphanumeric and followed by punctuation:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example359()
        {
            // Example 359
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     a_"foo"_
            //
            // Should be rendered as:
            //     <p>a_&quot;foo&quot;_</p>

            TestParser.TestSpec("a_\"foo\"_", "<p>a_&quot;foo&quot;_</p>", "", context: "Example 359\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Emphasis with `_` is not allowed inside words:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example360()
        {
            // Example 360
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo_bar_
            //
            // Should be rendered as:
            //     <p>foo_bar_</p>

            TestParser.TestSpec("foo_bar_", "<p>foo_bar_</p>", "", context: "Example 360\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example361()
        {
            // Example 361
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     5_6_78
            //
            // Should be rendered as:
            //     <p>5_6_78</p>

            TestParser.TestSpec("5_6_78", "<p>5_6_78</p>", "", context: "Example 361\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example362()
        {
            // Example 362
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     пристаням_стремятся_
            //
            // Should be rendered as:
            //     <p>пристаням_стремятся_</p>

            TestParser.TestSpec("пристаням_стремятся_", "<p>пристаням_стремятся_</p>", "", context: "Example 362\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Here `_` does not generate emphasis, because the first delimiter run
        // is right-flanking and the second left-flanking:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example363()
        {
            // Example 363
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     aa_"bb"_cc
            //
            // Should be rendered as:
            //     <p>aa_&quot;bb&quot;_cc</p>

            TestParser.TestSpec("aa_\"bb\"_cc", "<p>aa_&quot;bb&quot;_cc</p>", "", context: "Example 363\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is emphasis, even though the opening delimiter is
        // both left- and right-flanking, because it is preceded by
        // punctuation:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example364()
        {
            // Example 364
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo-_(bar)_
            //
            // Should be rendered as:
            //     <p>foo-<em>(bar)</em></p>

            TestParser.TestSpec("foo-_(bar)_", "<p>foo-<em>(bar)</em></p>", "", context: "Example 364\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 3:
        // 
        // This is not emphasis, because the closing delimiter does
        // not match the opening delimiter:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example365()
        {
            // Example 365
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo*
            //
            // Should be rendered as:
            //     <p>_foo*</p>

            TestParser.TestSpec("_foo*", "<p>_foo*</p>", "", context: "Example 365\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not emphasis, because the closing `*` is preceded by
        // whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example366()
        {
            // Example 366
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo bar *
            //
            // Should be rendered as:
            //     <p>*foo bar *</p>

            TestParser.TestSpec("*foo bar *", "<p>*foo bar *</p>", "", context: "Example 366\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // A line ending also counts as whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example367()
        {
            // Example 367
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo bar
            //     *
            //
            // Should be rendered as:
            //     <p>*foo bar
            //     *</p>

            TestParser.TestSpec("*foo bar\n*", "<p>*foo bar\n*</p>", "", context: "Example 367\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not emphasis, because the second `*` is
        // preceded by punctuation and followed by an alphanumeric
        // (hence it is not part of a [right-flanking delimiter run]:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example368()
        {
            // Example 368
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *(*foo)
            //
            // Should be rendered as:
            //     <p>*(*foo)</p>

            TestParser.TestSpec("*(*foo)", "<p>*(*foo)</p>", "", context: "Example 368\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // The point of this restriction is more easily appreciated
        // with this example:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example369()
        {
            // Example 369
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *(*foo*)*
            //
            // Should be rendered as:
            //     <p><em>(<em>foo</em>)</em></p>

            TestParser.TestSpec("*(*foo*)*", "<p><em>(<em>foo</em>)</em></p>", "", context: "Example 369\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Intraword emphasis with `*` is allowed:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example370()
        {
            // Example 370
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo*bar
            //
            // Should be rendered as:
            //     <p><em>foo</em>bar</p>

            TestParser.TestSpec("*foo*bar", "<p><em>foo</em>bar</p>", "", context: "Example 370\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 4:
        // 
        // This is not emphasis, because the closing `_` is preceded by
        // whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example371()
        {
            // Example 371
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo bar _
            //
            // Should be rendered as:
            //     <p>_foo bar _</p>

            TestParser.TestSpec("_foo bar _", "<p>_foo bar _</p>", "", context: "Example 371\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not emphasis, because the second `_` is
        // preceded by punctuation and followed by an alphanumeric:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example372()
        {
            // Example 372
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _(_foo)
            //
            // Should be rendered as:
            //     <p>_(_foo)</p>

            TestParser.TestSpec("_(_foo)", "<p>_(_foo)</p>", "", context: "Example 372\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is emphasis within emphasis:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example373()
        {
            // Example 373
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _(_foo_)_
            //
            // Should be rendered as:
            //     <p><em>(<em>foo</em>)</em></p>

            TestParser.TestSpec("_(_foo_)_", "<p><em>(<em>foo</em>)</em></p>", "", context: "Example 373\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Intraword emphasis is disallowed for `_`:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example374()
        {
            // Example 374
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo_bar
            //
            // Should be rendered as:
            //     <p>_foo_bar</p>

            TestParser.TestSpec("_foo_bar", "<p>_foo_bar</p>", "", context: "Example 374\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example375()
        {
            // Example 375
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _пристаням_стремятся
            //
            // Should be rendered as:
            //     <p>_пристаням_стремятся</p>

            TestParser.TestSpec("_пристаням_стремятся", "<p>_пристаням_стремятся</p>", "", context: "Example 375\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example376()
        {
            // Example 376
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo_bar_baz_
            //
            // Should be rendered as:
            //     <p><em>foo_bar_baz</em></p>

            TestParser.TestSpec("_foo_bar_baz_", "<p><em>foo_bar_baz</em></p>", "", context: "Example 376\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is emphasis, even though the closing delimiter is
        // both left- and right-flanking, because it is followed by
        // punctuation:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example377()
        {
            // Example 377
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _(bar)_.
            //
            // Should be rendered as:
            //     <p><em>(bar)</em>.</p>

            TestParser.TestSpec("_(bar)_.", "<p><em>(bar)</em>.</p>", "", context: "Example 377\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 5:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example378()
        {
            // Example 378
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo bar**
            //
            // Should be rendered as:
            //     <p><strong>foo bar</strong></p>

            TestParser.TestSpec("**foo bar**", "<p><strong>foo bar</strong></p>", "", context: "Example 378\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not strong emphasis, because the opening delimiter is
        // followed by whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example379()
        {
            // Example 379
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ** foo bar**
            //
            // Should be rendered as:
            //     <p>** foo bar**</p>

            TestParser.TestSpec("** foo bar**", "<p>** foo bar**</p>", "", context: "Example 379\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not strong emphasis, because the opening `**` is preceded
        // by an alphanumeric and followed by punctuation, and hence
        // not part of a [left-flanking delimiter run]:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example380()
        {
            // Example 380
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     a**"foo"**
            //
            // Should be rendered as:
            //     <p>a**&quot;foo&quot;**</p>

            TestParser.TestSpec("a**\"foo\"**", "<p>a**&quot;foo&quot;**</p>", "", context: "Example 380\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Intraword strong emphasis with `**` is permitted:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example381()
        {
            // Example 381
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo**bar**
            //
            // Should be rendered as:
            //     <p>foo<strong>bar</strong></p>

            TestParser.TestSpec("foo**bar**", "<p>foo<strong>bar</strong></p>", "", context: "Example 381\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 6:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example382()
        {
            // Example 382
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo bar__
            //
            // Should be rendered as:
            //     <p><strong>foo bar</strong></p>

            TestParser.TestSpec("__foo bar__", "<p><strong>foo bar</strong></p>", "", context: "Example 382\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not strong emphasis, because the opening delimiter is
        // followed by whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example383()
        {
            // Example 383
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __ foo bar__
            //
            // Should be rendered as:
            //     <p>__ foo bar__</p>

            TestParser.TestSpec("__ foo bar__", "<p>__ foo bar__</p>", "", context: "Example 383\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // A line ending counts as whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example384()
        {
            // Example 384
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __
            //     foo bar__
            //
            // Should be rendered as:
            //     <p>__
            //     foo bar__</p>

            TestParser.TestSpec("__\nfoo bar__", "<p>__\nfoo bar__</p>", "", context: "Example 384\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not strong emphasis, because the opening `__` is preceded
        // by an alphanumeric and followed by punctuation:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example385()
        {
            // Example 385
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     a__"foo"__
            //
            // Should be rendered as:
            //     <p>a__&quot;foo&quot;__</p>

            TestParser.TestSpec("a__\"foo\"__", "<p>a__&quot;foo&quot;__</p>", "", context: "Example 385\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Intraword strong emphasis is forbidden with `__`:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example386()
        {
            // Example 386
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo__bar__
            //
            // Should be rendered as:
            //     <p>foo__bar__</p>

            TestParser.TestSpec("foo__bar__", "<p>foo__bar__</p>", "", context: "Example 386\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example387()
        {
            // Example 387
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     5__6__78
            //
            // Should be rendered as:
            //     <p>5__6__78</p>

            TestParser.TestSpec("5__6__78", "<p>5__6__78</p>", "", context: "Example 387\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example388()
        {
            // Example 388
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     пристаням__стремятся__
            //
            // Should be rendered as:
            //     <p>пристаням__стремятся__</p>

            TestParser.TestSpec("пристаням__стремятся__", "<p>пристаням__стремятся__</p>", "", context: "Example 388\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example389()
        {
            // Example 389
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo, __bar__, baz__
            //
            // Should be rendered as:
            //     <p><strong>foo, <strong>bar</strong>, baz</strong></p>

            TestParser.TestSpec("__foo, __bar__, baz__", "<p><strong>foo, <strong>bar</strong>, baz</strong></p>", "", context: "Example 389\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is strong emphasis, even though the opening delimiter is
        // both left- and right-flanking, because it is preceded by
        // punctuation:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example390()
        {
            // Example 390
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo-__(bar)__
            //
            // Should be rendered as:
            //     <p>foo-<strong>(bar)</strong></p>

            TestParser.TestSpec("foo-__(bar)__", "<p>foo-<strong>(bar)</strong></p>", "", context: "Example 390\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 7:
        // 
        // This is not strong emphasis, because the closing delimiter is preceded
        // by whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example391()
        {
            // Example 391
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo bar **
            //
            // Should be rendered as:
            //     <p>**foo bar **</p>

            TestParser.TestSpec("**foo bar **", "<p>**foo bar **</p>", "", context: "Example 391\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // (Nor can it be interpreted as an emphasized `*foo bar *`, because of
        // Rule 11.)
        // 
        // This is not strong emphasis, because the second `**` is
        // preceded by punctuation and followed by an alphanumeric:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example392()
        {
            // Example 392
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **(**foo)
            //
            // Should be rendered as:
            //     <p>**(**foo)</p>

            TestParser.TestSpec("**(**foo)", "<p>**(**foo)</p>", "", context: "Example 392\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // The point of this restriction is more easily appreciated
        // with these examples:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example393()
        {
            // Example 393
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *(**foo**)*
            //
            // Should be rendered as:
            //     <p><em>(<strong>foo</strong>)</em></p>

            TestParser.TestSpec("*(**foo**)*", "<p><em>(<strong>foo</strong>)</em></p>", "", context: "Example 393\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example394()
        {
            // Example 394
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **Gomphocarpus (*Gomphocarpus physocarpus*, syn.
            //     *Asclepias physocarpa*)**
            //
            // Should be rendered as:
            //     <p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.
            //     <em>Asclepias physocarpa</em>)</strong></p>

            TestParser.TestSpec("**Gomphocarpus (*Gomphocarpus physocarpus*, syn.\n*Asclepias physocarpa*)**", "<p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.\n<em>Asclepias physocarpa</em>)</strong></p>", "", context: "Example 394\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example395()
        {
            // Example 395
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo "*bar*" foo**
            //
            // Should be rendered as:
            //     <p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>

            TestParser.TestSpec("**foo \"*bar*\" foo**", "<p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>", "", context: "Example 395\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Intraword emphasis:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example396()
        {
            // Example 396
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo**bar
            //
            // Should be rendered as:
            //     <p><strong>foo</strong>bar</p>

            TestParser.TestSpec("**foo**bar", "<p><strong>foo</strong>bar</p>", "", context: "Example 396\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 8:
        // 
        // This is not strong emphasis, because the closing delimiter is
        // preceded by whitespace:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example397()
        {
            // Example 397
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo bar __
            //
            // Should be rendered as:
            //     <p>__foo bar __</p>

            TestParser.TestSpec("__foo bar __", "<p>__foo bar __</p>", "", context: "Example 397\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is not strong emphasis, because the second `__` is
        // preceded by punctuation and followed by an alphanumeric:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example398()
        {
            // Example 398
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __(__foo)
            //
            // Should be rendered as:
            //     <p>__(__foo)</p>

            TestParser.TestSpec("__(__foo)", "<p>__(__foo)</p>", "", context: "Example 398\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // The point of this restriction is more easily appreciated
        // with this example:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example399()
        {
            // Example 399
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _(__foo__)_
            //
            // Should be rendered as:
            //     <p><em>(<strong>foo</strong>)</em></p>

            TestParser.TestSpec("_(__foo__)_", "<p><em>(<strong>foo</strong>)</em></p>", "", context: "Example 399\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Intraword strong emphasis is forbidden with `__`:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example400()
        {
            // Example 400
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo__bar
            //
            // Should be rendered as:
            //     <p>__foo__bar</p>

            TestParser.TestSpec("__foo__bar", "<p>__foo__bar</p>", "", context: "Example 400\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example401()
        {
            // Example 401
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __пристаням__стремятся
            //
            // Should be rendered as:
            //     <p>__пристаням__стремятся</p>

            TestParser.TestSpec("__пристаням__стремятся", "<p>__пристаням__стремятся</p>", "", context: "Example 401\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example402()
        {
            // Example 402
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo__bar__baz__
            //
            // Should be rendered as:
            //     <p><strong>foo__bar__baz</strong></p>

            TestParser.TestSpec("__foo__bar__baz__", "<p><strong>foo__bar__baz</strong></p>", "", context: "Example 402\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // This is strong emphasis, even though the closing delimiter is
        // both left- and right-flanking, because it is followed by
        // punctuation:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example403()
        {
            // Example 403
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __(bar)__.
            //
            // Should be rendered as:
            //     <p><strong>(bar)</strong>.</p>

            TestParser.TestSpec("__(bar)__.", "<p><strong>(bar)</strong>.</p>", "", context: "Example 403\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 9:
        // 
        // Any nonempty sequence of inline elements can be the contents of an
        // emphasized span.
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example404()
        {
            // Example 404
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo [bar](/url)*
            //
            // Should be rendered as:
            //     <p><em>foo <a href="/url">bar</a></em></p>

            TestParser.TestSpec("*foo [bar](/url)*", "<p><em>foo <a href=\"/url\">bar</a></em></p>", "", context: "Example 404\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example405()
        {
            // Example 405
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo
            //     bar*
            //
            // Should be rendered as:
            //     <p><em>foo
            //     bar</em></p>

            TestParser.TestSpec("*foo\nbar*", "<p><em>foo\nbar</em></p>", "", context: "Example 405\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // In particular, emphasis and strong emphasis can be nested
        // inside emphasis:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example406()
        {
            // Example 406
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo __bar__ baz_
            //
            // Should be rendered as:
            //     <p><em>foo <strong>bar</strong> baz</em></p>

            TestParser.TestSpec("_foo __bar__ baz_", "<p><em>foo <strong>bar</strong> baz</em></p>", "", context: "Example 406\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example407()
        {
            // Example 407
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo _bar_ baz_
            //
            // Should be rendered as:
            //     <p><em>foo <em>bar</em> baz</em></p>

            TestParser.TestSpec("_foo _bar_ baz_", "<p><em>foo <em>bar</em> baz</em></p>", "", context: "Example 407\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example408()
        {
            // Example 408
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo_ bar_
            //
            // Should be rendered as:
            //     <p><em><em>foo</em> bar</em></p>

            TestParser.TestSpec("__foo_ bar_", "<p><em><em>foo</em> bar</em></p>", "", context: "Example 408\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example409()
        {
            // Example 409
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo *bar**
            //
            // Should be rendered as:
            //     <p><em>foo <em>bar</em></em></p>

            TestParser.TestSpec("*foo *bar**", "<p><em>foo <em>bar</em></em></p>", "", context: "Example 409\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example410()
        {
            // Example 410
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo **bar** baz*
            //
            // Should be rendered as:
            //     <p><em>foo <strong>bar</strong> baz</em></p>

            TestParser.TestSpec("*foo **bar** baz*", "<p><em>foo <strong>bar</strong> baz</em></p>", "", context: "Example 410\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example411()
        {
            // Example 411
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo**bar**baz*
            //
            // Should be rendered as:
            //     <p><em>foo<strong>bar</strong>baz</em></p>

            TestParser.TestSpec("*foo**bar**baz*", "<p><em>foo<strong>bar</strong>baz</em></p>", "", context: "Example 411\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Note that in the preceding case, the interpretation
        // 
        // ``` markdown
        // <p><em>foo</em><em>bar<em></em>baz</em></p>
        // ```
        // 
        // 
        // is precluded by the condition that a delimiter that
        // can both open and close (like the `*` after `foo`)
        // cannot form emphasis if the sum of the lengths of
        // the delimiter runs containing the opening and
        // closing delimiters is a multiple of 3 unless
        // both lengths are multiples of 3.
        // 
        // 
        // For the same reason, we don't get two consecutive
        // emphasis sections in this example:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example412()
        {
            // Example 412
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo**bar*
            //
            // Should be rendered as:
            //     <p><em>foo**bar</em></p>

            TestParser.TestSpec("*foo**bar*", "<p><em>foo**bar</em></p>", "", context: "Example 412\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // The same condition ensures that the following
        // cases are all strong emphasis nested inside
        // emphasis, even when the interior whitespace is
        // omitted:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example413()
        {
            // Example 413
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ***foo** bar*
            //
            // Should be rendered as:
            //     <p><em><strong>foo</strong> bar</em></p>

            TestParser.TestSpec("***foo** bar*", "<p><em><strong>foo</strong> bar</em></p>", "", context: "Example 413\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example414()
        {
            // Example 414
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo **bar***
            //
            // Should be rendered as:
            //     <p><em>foo <strong>bar</strong></em></p>

            TestParser.TestSpec("*foo **bar***", "<p><em>foo <strong>bar</strong></em></p>", "", context: "Example 414\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example415()
        {
            // Example 415
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo**bar***
            //
            // Should be rendered as:
            //     <p><em>foo<strong>bar</strong></em></p>

            TestParser.TestSpec("*foo**bar***", "<p><em>foo<strong>bar</strong></em></p>", "", context: "Example 415\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // When the lengths of the interior closing and opening
        // delimiter runs are *both* multiples of 3, though,
        // they can match to create emphasis:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example416()
        {
            // Example 416
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo***bar***baz
            //
            // Should be rendered as:
            //     <p>foo<em><strong>bar</strong></em>baz</p>

            TestParser.TestSpec("foo***bar***baz", "<p>foo<em><strong>bar</strong></em>baz</p>", "", context: "Example 416\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example417()
        {
            // Example 417
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo******bar*********baz
            //
            // Should be rendered as:
            //     <p>foo<strong><strong><strong>bar</strong></strong></strong>***baz</p>

            TestParser.TestSpec("foo******bar*********baz", "<p>foo<strong><strong><strong>bar</strong></strong></strong>***baz</p>", "", context: "Example 417\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Indefinite levels of nesting are possible:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example418()
        {
            // Example 418
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo **bar *baz* bim** bop*
            //
            // Should be rendered as:
            //     <p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>

            TestParser.TestSpec("*foo **bar *baz* bim** bop*", "<p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>", "", context: "Example 418\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example419()
        {
            // Example 419
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo [*bar*](/url)*
            //
            // Should be rendered as:
            //     <p><em>foo <a href="/url"><em>bar</em></a></em></p>

            TestParser.TestSpec("*foo [*bar*](/url)*", "<p><em>foo <a href=\"/url\"><em>bar</em></a></em></p>", "", context: "Example 419\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // There can be no empty emphasis or strong emphasis:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example420()
        {
            // Example 420
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ** is not an empty emphasis
            //
            // Should be rendered as:
            //     <p>** is not an empty emphasis</p>

            TestParser.TestSpec("** is not an empty emphasis", "<p>** is not an empty emphasis</p>", "", context: "Example 420\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example421()
        {
            // Example 421
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **** is not an empty strong emphasis
            //
            // Should be rendered as:
            //     <p>**** is not an empty strong emphasis</p>

            TestParser.TestSpec("**** is not an empty strong emphasis", "<p>**** is not an empty strong emphasis</p>", "", context: "Example 421\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 10:
        // 
        // Any nonempty sequence of inline elements can be the contents of an
        // strongly emphasized span.
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example422()
        {
            // Example 422
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo [bar](/url)**
            //
            // Should be rendered as:
            //     <p><strong>foo <a href="/url">bar</a></strong></p>

            TestParser.TestSpec("**foo [bar](/url)**", "<p><strong>foo <a href=\"/url\">bar</a></strong></p>", "", context: "Example 422\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example423()
        {
            // Example 423
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo
            //     bar**
            //
            // Should be rendered as:
            //     <p><strong>foo
            //     bar</strong></p>

            TestParser.TestSpec("**foo\nbar**", "<p><strong>foo\nbar</strong></p>", "", context: "Example 423\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // In particular, emphasis and strong emphasis can be nested
        // inside strong emphasis:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example424()
        {
            // Example 424
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo _bar_ baz__
            //
            // Should be rendered as:
            //     <p><strong>foo <em>bar</em> baz</strong></p>

            TestParser.TestSpec("__foo _bar_ baz__", "<p><strong>foo <em>bar</em> baz</strong></p>", "", context: "Example 424\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example425()
        {
            // Example 425
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo __bar__ baz__
            //
            // Should be rendered as:
            //     <p><strong>foo <strong>bar</strong> baz</strong></p>

            TestParser.TestSpec("__foo __bar__ baz__", "<p><strong>foo <strong>bar</strong> baz</strong></p>", "", context: "Example 425\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example426()
        {
            // Example 426
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ____foo__ bar__
            //
            // Should be rendered as:
            //     <p><strong><strong>foo</strong> bar</strong></p>

            TestParser.TestSpec("____foo__ bar__", "<p><strong><strong>foo</strong> bar</strong></p>", "", context: "Example 426\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example427()
        {
            // Example 427
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo **bar****
            //
            // Should be rendered as:
            //     <p><strong>foo <strong>bar</strong></strong></p>

            TestParser.TestSpec("**foo **bar****", "<p><strong>foo <strong>bar</strong></strong></p>", "", context: "Example 427\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example428()
        {
            // Example 428
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo *bar* baz**
            //
            // Should be rendered as:
            //     <p><strong>foo <em>bar</em> baz</strong></p>

            TestParser.TestSpec("**foo *bar* baz**", "<p><strong>foo <em>bar</em> baz</strong></p>", "", context: "Example 428\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example429()
        {
            // Example 429
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo*bar*baz**
            //
            // Should be rendered as:
            //     <p><strong>foo<em>bar</em>baz</strong></p>

            TestParser.TestSpec("**foo*bar*baz**", "<p><strong>foo<em>bar</em>baz</strong></p>", "", context: "Example 429\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example430()
        {
            // Example 430
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ***foo* bar**
            //
            // Should be rendered as:
            //     <p><strong><em>foo</em> bar</strong></p>

            TestParser.TestSpec("***foo* bar**", "<p><strong><em>foo</em> bar</strong></p>", "", context: "Example 430\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example431()
        {
            // Example 431
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo *bar***
            //
            // Should be rendered as:
            //     <p><strong>foo <em>bar</em></strong></p>

            TestParser.TestSpec("**foo *bar***", "<p><strong>foo <em>bar</em></strong></p>", "", context: "Example 431\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Indefinite levels of nesting are possible:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example432()
        {
            // Example 432
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo *bar **baz**
            //     bim* bop**
            //
            // Should be rendered as:
            //     <p><strong>foo <em>bar <strong>baz</strong>
            //     bim</em> bop</strong></p>

            TestParser.TestSpec("**foo *bar **baz**\nbim* bop**", "<p><strong>foo <em>bar <strong>baz</strong>\nbim</em> bop</strong></p>", "", context: "Example 432\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example433()
        {
            // Example 433
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo [*bar*](/url)**
            //
            // Should be rendered as:
            //     <p><strong>foo <a href="/url"><em>bar</em></a></strong></p>

            TestParser.TestSpec("**foo [*bar*](/url)**", "<p><strong>foo <a href=\"/url\"><em>bar</em></a></strong></p>", "", context: "Example 433\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // There can be no empty emphasis or strong emphasis:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example434()
        {
            // Example 434
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __ is not an empty emphasis
            //
            // Should be rendered as:
            //     <p>__ is not an empty emphasis</p>

            TestParser.TestSpec("__ is not an empty emphasis", "<p>__ is not an empty emphasis</p>", "", context: "Example 434\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example435()
        {
            // Example 435
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ____ is not an empty strong emphasis
            //
            // Should be rendered as:
            //     <p>____ is not an empty strong emphasis</p>

            TestParser.TestSpec("____ is not an empty strong emphasis", "<p>____ is not an empty strong emphasis</p>", "", context: "Example 435\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 11:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example436()
        {
            // Example 436
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo ***
            //
            // Should be rendered as:
            //     <p>foo ***</p>

            TestParser.TestSpec("foo ***", "<p>foo ***</p>", "", context: "Example 436\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example437()
        {
            // Example 437
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo *\**
            //
            // Should be rendered as:
            //     <p>foo <em>*</em></p>

            TestParser.TestSpec("foo *\\**", "<p>foo <em>*</em></p>", "", context: "Example 437\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example438()
        {
            // Example 438
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo *_*
            //
            // Should be rendered as:
            //     <p>foo <em>_</em></p>

            TestParser.TestSpec("foo *_*", "<p>foo <em>_</em></p>", "", context: "Example 438\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example439()
        {
            // Example 439
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo *****
            //
            // Should be rendered as:
            //     <p>foo *****</p>

            TestParser.TestSpec("foo *****", "<p>foo *****</p>", "", context: "Example 439\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example440()
        {
            // Example 440
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo **\***
            //
            // Should be rendered as:
            //     <p>foo <strong>*</strong></p>

            TestParser.TestSpec("foo **\\***", "<p>foo <strong>*</strong></p>", "", context: "Example 440\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example441()
        {
            // Example 441
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo **_**
            //
            // Should be rendered as:
            //     <p>foo <strong>_</strong></p>

            TestParser.TestSpec("foo **_**", "<p>foo <strong>_</strong></p>", "", context: "Example 441\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Note that when delimiters do not match evenly, Rule 11 determines
        // that the excess literal `*` characters will appear outside of the
        // emphasis, rather than inside it:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example442()
        {
            // Example 442
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo*
            //
            // Should be rendered as:
            //     <p>*<em>foo</em></p>

            TestParser.TestSpec("**foo*", "<p>*<em>foo</em></p>", "", context: "Example 442\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example443()
        {
            // Example 443
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo**
            //
            // Should be rendered as:
            //     <p><em>foo</em>*</p>

            TestParser.TestSpec("*foo**", "<p><em>foo</em>*</p>", "", context: "Example 443\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example444()
        {
            // Example 444
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ***foo**
            //
            // Should be rendered as:
            //     <p>*<strong>foo</strong></p>

            TestParser.TestSpec("***foo**", "<p>*<strong>foo</strong></p>", "", context: "Example 444\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example445()
        {
            // Example 445
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ****foo*
            //
            // Should be rendered as:
            //     <p>***<em>foo</em></p>

            TestParser.TestSpec("****foo*", "<p>***<em>foo</em></p>", "", context: "Example 445\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example446()
        {
            // Example 446
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo***
            //
            // Should be rendered as:
            //     <p><strong>foo</strong>*</p>

            TestParser.TestSpec("**foo***", "<p><strong>foo</strong>*</p>", "", context: "Example 446\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example447()
        {
            // Example 447
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo****
            //
            // Should be rendered as:
            //     <p><em>foo</em>***</p>

            TestParser.TestSpec("*foo****", "<p><em>foo</em>***</p>", "", context: "Example 447\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 12:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example448()
        {
            // Example 448
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo ___
            //
            // Should be rendered as:
            //     <p>foo ___</p>

            TestParser.TestSpec("foo ___", "<p>foo ___</p>", "", context: "Example 448\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example449()
        {
            // Example 449
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo _\__
            //
            // Should be rendered as:
            //     <p>foo <em>_</em></p>

            TestParser.TestSpec("foo _\\__", "<p>foo <em>_</em></p>", "", context: "Example 449\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example450()
        {
            // Example 450
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo _*_
            //
            // Should be rendered as:
            //     <p>foo <em>*</em></p>

            TestParser.TestSpec("foo _*_", "<p>foo <em>*</em></p>", "", context: "Example 450\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example451()
        {
            // Example 451
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo _____
            //
            // Should be rendered as:
            //     <p>foo _____</p>

            TestParser.TestSpec("foo _____", "<p>foo _____</p>", "", context: "Example 451\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example452()
        {
            // Example 452
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo __\___
            //
            // Should be rendered as:
            //     <p>foo <strong>_</strong></p>

            TestParser.TestSpec("foo __\\___", "<p>foo <strong>_</strong></p>", "", context: "Example 452\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example453()
        {
            // Example 453
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     foo __*__
            //
            // Should be rendered as:
            //     <p>foo <strong>*</strong></p>

            TestParser.TestSpec("foo __*__", "<p>foo <strong>*</strong></p>", "", context: "Example 453\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example454()
        {
            // Example 454
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo_
            //
            // Should be rendered as:
            //     <p>_<em>foo</em></p>

            TestParser.TestSpec("__foo_", "<p>_<em>foo</em></p>", "", context: "Example 454\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Note that when delimiters do not match evenly, Rule 12 determines
        // that the excess literal `_` characters will appear outside of the
        // emphasis, rather than inside it:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example455()
        {
            // Example 455
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo__
            //
            // Should be rendered as:
            //     <p><em>foo</em>_</p>

            TestParser.TestSpec("_foo__", "<p><em>foo</em>_</p>", "", context: "Example 455\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example456()
        {
            // Example 456
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ___foo__
            //
            // Should be rendered as:
            //     <p>_<strong>foo</strong></p>

            TestParser.TestSpec("___foo__", "<p>_<strong>foo</strong></p>", "", context: "Example 456\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example457()
        {
            // Example 457
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ____foo_
            //
            // Should be rendered as:
            //     <p>___<em>foo</em></p>

            TestParser.TestSpec("____foo_", "<p>___<em>foo</em></p>", "", context: "Example 457\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example458()
        {
            // Example 458
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo___
            //
            // Should be rendered as:
            //     <p><strong>foo</strong>_</p>

            TestParser.TestSpec("__foo___", "<p><strong>foo</strong>_</p>", "", context: "Example 458\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example459()
        {
            // Example 459
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo____
            //
            // Should be rendered as:
            //     <p><em>foo</em>___</p>

            TestParser.TestSpec("_foo____", "<p><em>foo</em>___</p>", "", context: "Example 459\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 13 implies that if you want emphasis nested directly inside
        // emphasis, you must use different delimiters:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example460()
        {
            // Example 460
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo**
            //
            // Should be rendered as:
            //     <p><strong>foo</strong></p>

            TestParser.TestSpec("**foo**", "<p><strong>foo</strong></p>", "", context: "Example 460\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example461()
        {
            // Example 461
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *_foo_*
            //
            // Should be rendered as:
            //     <p><em><em>foo</em></em></p>

            TestParser.TestSpec("*_foo_*", "<p><em><em>foo</em></em></p>", "", context: "Example 461\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example462()
        {
            // Example 462
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __foo__
            //
            // Should be rendered as:
            //     <p><strong>foo</strong></p>

            TestParser.TestSpec("__foo__", "<p><strong>foo</strong></p>", "", context: "Example 462\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example463()
        {
            // Example 463
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _*foo*_
            //
            // Should be rendered as:
            //     <p><em><em>foo</em></em></p>

            TestParser.TestSpec("_*foo*_", "<p><em><em>foo</em></em></p>", "", context: "Example 463\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // However, strong emphasis within strong emphasis is possible without
        // switching delimiters:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example464()
        {
            // Example 464
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ****foo****
            //
            // Should be rendered as:
            //     <p><strong><strong>foo</strong></strong></p>

            TestParser.TestSpec("****foo****", "<p><strong><strong>foo</strong></strong></p>", "", context: "Example 464\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example465()
        {
            // Example 465
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ____foo____
            //
            // Should be rendered as:
            //     <p><strong><strong>foo</strong></strong></p>

            TestParser.TestSpec("____foo____", "<p><strong><strong>foo</strong></strong></p>", "", context: "Example 465\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 13 can be applied to arbitrarily long sequences of
        // delimiters:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example466()
        {
            // Example 466
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ******foo******
            //
            // Should be rendered as:
            //     <p><strong><strong><strong>foo</strong></strong></strong></p>

            TestParser.TestSpec("******foo******", "<p><strong><strong><strong>foo</strong></strong></strong></p>", "", context: "Example 466\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 14:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example467()
        {
            // Example 467
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     ***foo***
            //
            // Should be rendered as:
            //     <p><em><strong>foo</strong></em></p>

            TestParser.TestSpec("***foo***", "<p><em><strong>foo</strong></em></p>", "", context: "Example 467\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example468()
        {
            // Example 468
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _____foo_____
            //
            // Should be rendered as:
            //     <p><em><strong><strong>foo</strong></strong></em></p>

            TestParser.TestSpec("_____foo_____", "<p><em><strong><strong>foo</strong></strong></em></p>", "", context: "Example 468\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 15:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example469()
        {
            // Example 469
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo _bar* baz_
            //
            // Should be rendered as:
            //     <p><em>foo _bar</em> baz_</p>

            TestParser.TestSpec("*foo _bar* baz_", "<p><em>foo _bar</em> baz_</p>", "", context: "Example 469\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example470()
        {
            // Example 470
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo __bar *baz bim__ bam*
            //
            // Should be rendered as:
            //     <p><em>foo <strong>bar *baz bim</strong> bam</em></p>

            TestParser.TestSpec("*foo __bar *baz bim__ bam*", "<p><em>foo <strong>bar *baz bim</strong> bam</em></p>", "", context: "Example 470\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 16:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example471()
        {
            // Example 471
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **foo **bar baz**
            //
            // Should be rendered as:
            //     <p>**foo <strong>bar baz</strong></p>

            TestParser.TestSpec("**foo **bar baz**", "<p>**foo <strong>bar baz</strong></p>", "", context: "Example 471\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example472()
        {
            // Example 472
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *foo *bar baz*
            //
            // Should be rendered as:
            //     <p>*foo <em>bar baz</em></p>

            TestParser.TestSpec("*foo *bar baz*", "<p>*foo <em>bar baz</em></p>", "", context: "Example 472\nSection Inlines / Emphasis and strong emphasis\n");
        }

        // Rule 17:
        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example473()
        {
            // Example 473
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *[bar*](/url)
            //
            // Should be rendered as:
            //     <p>*<a href="/url">bar*</a></p>

            TestParser.TestSpec("*[bar*](/url)", "<p>*<a href=\"/url\">bar*</a></p>", "", context: "Example 473\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example474()
        {
            // Example 474
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _foo [bar_](/url)
            //
            // Should be rendered as:
            //     <p>_foo <a href="/url">bar_</a></p>

            TestParser.TestSpec("_foo [bar_](/url)", "<p>_foo <a href=\"/url\">bar_</a></p>", "", context: "Example 474\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example475()
        {
            // Example 475
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *<img src="foo" title="*"/>
            //
            // Should be rendered as:
            //     <p>*<img src="foo" title="*"/></p>

            TestParser.TestSpec("*<img src=\"foo\" title=\"*\"/>", "<p>*<img src=\"foo\" title=\"*\"/></p>", "", context: "Example 475\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example476()
        {
            // Example 476
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **<a href="**">
            //
            // Should be rendered as:
            //     <p>**<a href="**"></p>

            TestParser.TestSpec("**<a href=\"**\">", "<p>**<a href=\"**\"></p>", "", context: "Example 476\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example477()
        {
            // Example 477
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __<a href="__">
            //
            // Should be rendered as:
            //     <p>__<a href="__"></p>

            TestParser.TestSpec("__<a href=\"__\">", "<p>__<a href=\"__\"></p>", "", context: "Example 477\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example478()
        {
            // Example 478
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     *a `*`*
            //
            // Should be rendered as:
            //     <p><em>a <code>*</code></em></p>

            TestParser.TestSpec("*a `*`*", "<p><em>a <code>*</code></em></p>", "", context: "Example 478\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example479()
        {
            // Example 479
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     _a `_`_
            //
            // Should be rendered as:
            //     <p><em>a <code>_</code></em></p>

            TestParser.TestSpec("_a `_`_", "<p><em>a <code>_</code></em></p>", "", context: "Example 479\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example480()
        {
            // Example 480
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     **a<https://foo.bar/?q=**>
            //
            // Should be rendered as:
            //     <p>**a<a href="https://foo.bar/?q=**">https://foo.bar/?q=**</a></p>

            TestParser.TestSpec("**a<https://foo.bar/?q=**>", "<p>**a<a href=\"https://foo.bar/?q=**\">https://foo.bar/?q=**</a></p>", "", context: "Example 480\nSection Inlines / Emphasis and strong emphasis\n");
        }

        [Test]
        public void InlinesEmphasisAndStrongEmphasis_Example481()
        {
            // Example 481
            // Section: Inlines / Emphasis and strong emphasis
            //
            // The following Markdown:
            //     __a<https://foo.bar/?q=__>
            //
            // Should be rendered as:
            //     <p>__a<a href="https://foo.bar/?q=__">https://foo.bar/?q=__</a></p>

            TestParser.TestSpec("__a<https://foo.bar/?q=__>", "<p>__a<a href=\"https://foo.bar/?q=__\">https://foo.bar/?q=__</a></p>", "", context: "Example 481\nSection Inlines / Emphasis and strong emphasis\n");
        }
    }

    [TestFixture]
    public class TestInlinesLinks
    {
        // ## Links
        // 
        // A link contains [link text] (the visible text), a [link destination]
        // (the URI that is the link destination), and optionally a [link title].
        // There are two basic kinds of links in Markdown.  In [inline links] the
        // destination and title are given immediately after the link text.  In
        // [reference links] the destination and title are defined elsewhere in
        // the document.
        // 
        // A [link text](@) consists of a sequence of zero or more
        // inline elements enclosed by square brackets (`[` and `]`).  The
        // following rules apply:
        // 
        // - Links may not contain other links, at any level of nesting. If
        //   multiple otherwise valid link definitions appear nested inside each
        //   other, the inner-most definition is used.
        // 
        // - Brackets are allowed in the [link text] only if (a) they
        //   are backslash-escaped or (b) they appear as a matched pair of brackets,
        //   with an open bracket `[`, a sequence of zero or more inlines, and
        //   a close bracket `]`.
        // 
        // - Backtick [code spans], [autolinks], and raw [HTML tags] bind more tightly
        //   than the brackets in link text.  Thus, for example,
        //   `` [foo`]` `` could not be a link text, since the second `]`
        //   is part of a code span.
        // 
        // - The brackets in link text bind more tightly than markers for
        //   [emphasis and strong emphasis]. Thus, for example, `*[foo*](url)` is a link.
        // 
        // A [link destination](@) consists of either
        // 
        // - a sequence of zero or more characters between an opening `<` and a
        //   closing `>` that contains no line endings or unescaped
        //   `<` or `>` characters, or
        // 
        // - a nonempty sequence of characters that does not start with `<`,
        //   does not include [ASCII control characters][ASCII control character]
        //   or [space] character, and includes parentheses only if (a) they are
        //   backslash-escaped or (b) they are part of a balanced pair of
        //   unescaped parentheses.
        //   (Implementations may impose limits on parentheses nesting to
        //   avoid performance issues, but at least three levels of nesting
        //   should be supported.)
        // 
        // A [link title](@)  consists of either
        // 
        // - a sequence of zero or more characters between straight double-quote
        //   characters (`"`), including a `"` character only if it is
        //   backslash-escaped, or
        // 
        // - a sequence of zero or more characters between straight single-quote
        //   characters (`'`), including a `'` character only if it is
        //   backslash-escaped, or
        // 
        // - a sequence of zero or more characters between matching parentheses
        //   (`(...)`), including a `(` or `)` character only if it is
        //   backslash-escaped.
        // 
        // Although [link titles] may span multiple lines, they may not contain
        // a [blank line].
        // 
        // An [inline link](@) consists of a [link text] followed immediately
        // by a left parenthesis `(`, an optional [link destination], an optional
        // [link title], and a right parenthesis `)`.
        // These four components may be separated by spaces, tabs, and up to one line
        // ending.
        // If both [link destination] and [link title] are present, they *must* be
        // separated by spaces, tabs, and up to one line ending.
        // 
        // The link's text consists of the inlines contained
        // in the [link text] (excluding the enclosing square brackets).
        // The link's URI consists of the link destination, excluding enclosing
        // `<...>` if present, with backslash-escapes in effect as described
        // above.  The link's title consists of the link title, excluding its
        // enclosing delimiters, with backslash-escapes in effect as described
        // above.
        // 
        // Here is a simple inline link:
        [Test]
        public void InlinesLinks_Example482()
        {
            // Example 482
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/uri "title")
            //
            // Should be rendered as:
            //     <p><a href="/uri" title="title">link</a></p>

            TestParser.TestSpec("[link](/uri \"title\")", "<p><a href=\"/uri\" title=\"title\">link</a></p>", "", context: "Example 482\nSection Inlines / Links\n");
        }

        // The title, the link text and even 
        // the destination may be omitted:
        [Test]
        public void InlinesLinks_Example483()
        {
            // Example 483
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/uri)
            //
            // Should be rendered as:
            //     <p><a href="/uri">link</a></p>

            TestParser.TestSpec("[link](/uri)", "<p><a href=\"/uri\">link</a></p>", "", context: "Example 483\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example484()
        {
            // Example 484
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [](./target.md)
            //
            // Should be rendered as:
            //     <p><a href="./target.md"></a></p>

            TestParser.TestSpec("[](./target.md)", "<p><a href=\"./target.md\"></a></p>", "", context: "Example 484\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example485()
        {
            // Example 485
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link]()
            //
            // Should be rendered as:
            //     <p><a href="">link</a></p>

            TestParser.TestSpec("[link]()", "<p><a href=\"\">link</a></p>", "", context: "Example 485\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example486()
        {
            // Example 486
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](<>)
            //
            // Should be rendered as:
            //     <p><a href="">link</a></p>

            TestParser.TestSpec("[link](<>)", "<p><a href=\"\">link</a></p>", "", context: "Example 486\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example487()
        {
            // Example 487
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     []()
            //
            // Should be rendered as:
            //     <p><a href=""></a></p>

            TestParser.TestSpec("[]()", "<p><a href=\"\"></a></p>", "", context: "Example 487\nSection Inlines / Links\n");
        }

        // The destination can only contain spaces if it is
        // enclosed in pointy brackets:
        [Test]
        public void InlinesLinks_Example488()
        {
            // Example 488
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/my uri)
            //
            // Should be rendered as:
            //     <p>[link](/my uri)</p>

            TestParser.TestSpec("[link](/my uri)", "<p>[link](/my uri)</p>", "", context: "Example 488\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example489()
        {
            // Example 489
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](</my uri>)
            //
            // Should be rendered as:
            //     <p><a href="/my%20uri">link</a></p>

            TestParser.TestSpec("[link](</my uri>)", "<p><a href=\"/my%20uri\">link</a></p>", "", context: "Example 489\nSection Inlines / Links\n");
        }

        // The destination cannot contain line endings,
        // even if enclosed in pointy brackets:
        [Test]
        public void InlinesLinks_Example490()
        {
            // Example 490
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](foo
            //     bar)
            //
            // Should be rendered as:
            //     <p>[link](foo
            //     bar)</p>

            TestParser.TestSpec("[link](foo\nbar)", "<p>[link](foo\nbar)</p>", "", context: "Example 490\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example491()
        {
            // Example 491
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](<foo
            //     bar>)
            //
            // Should be rendered as:
            //     <p>[link](<foo
            //     bar>)</p>

            TestParser.TestSpec("[link](<foo\nbar>)", "<p>[link](<foo\nbar>)</p>", "", context: "Example 491\nSection Inlines / Links\n");
        }

        // The destination can contain `)` if it is enclosed
        // in pointy brackets:
        [Test]
        public void InlinesLinks_Example492()
        {
            // Example 492
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [a](<b)c>)
            //
            // Should be rendered as:
            //     <p><a href="b)c">a</a></p>

            TestParser.TestSpec("[a](<b)c>)", "<p><a href=\"b)c\">a</a></p>", "", context: "Example 492\nSection Inlines / Links\n");
        }

        // Pointy brackets that enclose links must be unescaped:
        [Test]
        public void InlinesLinks_Example493()
        {
            // Example 493
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](<foo\>)
            //
            // Should be rendered as:
            //     <p>[link](&lt;foo&gt;)</p>

            TestParser.TestSpec("[link](<foo\\>)", "<p>[link](&lt;foo&gt;)</p>", "", context: "Example 493\nSection Inlines / Links\n");
        }

        // These are not links, because the opening pointy bracket
        // is not matched properly:
        [Test]
        public void InlinesLinks_Example494()
        {
            // Example 494
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [a](<b)c
            //     [a](<b)c>
            //     [a](<b>c)
            //
            // Should be rendered as:
            //     <p>[a](&lt;b)c
            //     [a](&lt;b)c&gt;
            //     [a](<b>c)</p>

            TestParser.TestSpec("[a](<b)c\n[a](<b)c>\n[a](<b>c)", "<p>[a](&lt;b)c\n[a](&lt;b)c&gt;\n[a](<b>c)</p>", "", context: "Example 494\nSection Inlines / Links\n");
        }

        // Parentheses inside the link destination may be escaped:
        [Test]
        public void InlinesLinks_Example495()
        {
            // Example 495
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](\(foo\))
            //
            // Should be rendered as:
            //     <p><a href="(foo)">link</a></p>

            TestParser.TestSpec("[link](\\(foo\\))", "<p><a href=\"(foo)\">link</a></p>", "", context: "Example 495\nSection Inlines / Links\n");
        }

        // Any number of parentheses are allowed without escaping, as long as they are
        // balanced:
        [Test]
        public void InlinesLinks_Example496()
        {
            // Example 496
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](foo(and(bar)))
            //
            // Should be rendered as:
            //     <p><a href="foo(and(bar))">link</a></p>

            TestParser.TestSpec("[link](foo(and(bar)))", "<p><a href=\"foo(and(bar))\">link</a></p>", "", context: "Example 496\nSection Inlines / Links\n");
        }

        // However, if you have unbalanced parentheses, you need to escape or use the
        // `<...>` form:
        [Test]
        public void InlinesLinks_Example497()
        {
            // Example 497
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](foo(and(bar))
            //
            // Should be rendered as:
            //     <p>[link](foo(and(bar))</p>

            TestParser.TestSpec("[link](foo(and(bar))", "<p>[link](foo(and(bar))</p>", "", context: "Example 497\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example498()
        {
            // Example 498
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](foo\(and\(bar\))
            //
            // Should be rendered as:
            //     <p><a href="foo(and(bar)">link</a></p>

            TestParser.TestSpec("[link](foo\\(and\\(bar\\))", "<p><a href=\"foo(and(bar)\">link</a></p>", "", context: "Example 498\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example499()
        {
            // Example 499
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](<foo(and(bar)>)
            //
            // Should be rendered as:
            //     <p><a href="foo(and(bar)">link</a></p>

            TestParser.TestSpec("[link](<foo(and(bar)>)", "<p><a href=\"foo(and(bar)\">link</a></p>", "", context: "Example 499\nSection Inlines / Links\n");
        }

        // Parentheses and other symbols can also be escaped, as usual
        // in Markdown:
        [Test]
        public void InlinesLinks_Example500()
        {
            // Example 500
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](foo\)\:)
            //
            // Should be rendered as:
            //     <p><a href="foo):">link</a></p>

            TestParser.TestSpec("[link](foo\\)\\:)", "<p><a href=\"foo):\">link</a></p>", "", context: "Example 500\nSection Inlines / Links\n");
        }

        // A link can contain fragment identifiers and queries:
        [Test]
        public void InlinesLinks_Example501()
        {
            // Example 501
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](#fragment)
            //     
            //     [link](https://example.com#fragment)
            //     
            //     [link](https://example.com?foo=3#frag)
            //
            // Should be rendered as:
            //     <p><a href="#fragment">link</a></p>
            //     <p><a href="https://example.com#fragment">link</a></p>
            //     <p><a href="https://example.com?foo=3#frag">link</a></p>

            TestParser.TestSpec("[link](#fragment)\n\n[link](https://example.com#fragment)\n\n[link](https://example.com?foo=3#frag)", "<p><a href=\"#fragment\">link</a></p>\n<p><a href=\"https://example.com#fragment\">link</a></p>\n<p><a href=\"https://example.com?foo=3#frag\">link</a></p>", "", context: "Example 501\nSection Inlines / Links\n");
        }

        // Note that a backslash before a non-escapable character is
        // just a backslash:
        [Test]
        public void InlinesLinks_Example502()
        {
            // Example 502
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](foo\bar)
            //
            // Should be rendered as:
            //     <p><a href="foo%5Cbar">link</a></p>

            TestParser.TestSpec("[link](foo\\bar)", "<p><a href=\"foo%5Cbar\">link</a></p>", "", context: "Example 502\nSection Inlines / Links\n");
        }

        // URL-escaping should be left alone inside the destination, as all
        // URL-escaped characters are also valid URL characters. Entity and
        // numerical character references in the destination will be parsed
        // into the corresponding Unicode code points, as usual.  These may
        // be optionally URL-escaped when written as HTML, but this spec
        // does not enforce any particular policy for rendering URLs in
        // HTML or other formats.  Renderers may make different decisions
        // about how to escape or normalize URLs in the output.
        [Test]
        public void InlinesLinks_Example503()
        {
            // Example 503
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](foo%20b&auml;)
            //
            // Should be rendered as:
            //     <p><a href="foo%20b%C3%A4">link</a></p>

            TestParser.TestSpec("[link](foo%20b&auml;)", "<p><a href=\"foo%20b%C3%A4\">link</a></p>", "", context: "Example 503\nSection Inlines / Links\n");
        }

        // Note that, because titles can often be parsed as destinations,
        // if you try to omit the destination and keep the title, you'll
        // get unexpected results:
        [Test]
        public void InlinesLinks_Example504()
        {
            // Example 504
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link]("title")
            //
            // Should be rendered as:
            //     <p><a href="%22title%22">link</a></p>

            TestParser.TestSpec("[link](\"title\")", "<p><a href=\"%22title%22\">link</a></p>", "", context: "Example 504\nSection Inlines / Links\n");
        }

        // Titles may be in single quotes, double quotes, or parentheses:
        [Test]
        public void InlinesLinks_Example505()
        {
            // Example 505
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/url "title")
            //     [link](/url 'title')
            //     [link](/url (title))
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">link</a>
            //     <a href="/url" title="title">link</a>
            //     <a href="/url" title="title">link</a></p>

            TestParser.TestSpec("[link](/url \"title\")\n[link](/url 'title')\n[link](/url (title))", "<p><a href=\"/url\" title=\"title\">link</a>\n<a href=\"/url\" title=\"title\">link</a>\n<a href=\"/url\" title=\"title\">link</a></p>", "", context: "Example 505\nSection Inlines / Links\n");
        }

        // Backslash escapes and entity and numeric character references
        // may be used in titles:
        [Test]
        public void InlinesLinks_Example506()
        {
            // Example 506
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/url "title \"&quot;")
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title &quot;&quot;">link</a></p>

            TestParser.TestSpec("[link](/url \"title \\\"&quot;\")", "<p><a href=\"/url\" title=\"title &quot;&quot;\">link</a></p>", "", context: "Example 506\nSection Inlines / Links\n");
        }

        // Titles must be separated from the link using spaces, tabs, and up to one line
        // ending.
        // Other [Unicode whitespace] like non-breaking space doesn't work.
        [Test]
        public void InlinesLinks_Example507()
        {
            // Example 507
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/url "title")
            //
            // Should be rendered as:
            //     <p><a href="/url%C2%A0%22title%22">link</a></p>

            TestParser.TestSpec("[link](/url \"title\")", "<p><a href=\"/url%C2%A0%22title%22\">link</a></p>", "", context: "Example 507\nSection Inlines / Links\n");
        }

        // Nested balanced quotes are not allowed without escaping:
        [Test]
        public void InlinesLinks_Example508()
        {
            // Example 508
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/url "title "and" title")
            //
            // Should be rendered as:
            //     <p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>

            TestParser.TestSpec("[link](/url \"title \"and\" title\")", "<p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>", "", context: "Example 508\nSection Inlines / Links\n");
        }

        // But it is easy to work around this by using a different quote type:
        [Test]
        public void InlinesLinks_Example509()
        {
            // Example 509
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](/url 'title "and" title')
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title &quot;and&quot; title">link</a></p>

            TestParser.TestSpec("[link](/url 'title \"and\" title')", "<p><a href=\"/url\" title=\"title &quot;and&quot; title\">link</a></p>", "", context: "Example 509\nSection Inlines / Links\n");
        }

        // (Note:  `Markdown.pl` did allow double quotes inside a double-quoted
        // title, and its test suite included a test demonstrating this.
        // But it is hard to see a good rationale for the extra complexity this
        // brings, since there are already many ways---backslash escaping,
        // entity and numeric character references, or using a different
        // quote type for the enclosing title---to write titles containing
        // double quotes.  `Markdown.pl`'s handling of titles has a number
        // of other strange features.  For example, it allows single-quoted
        // titles in inline links, but not reference links.  And, in
        // reference links but not inline links, it allows a title to begin
        // with `"` and end with `)`.  `Markdown.pl` 1.0.1 even allows
        // titles with no closing quotation mark, though 1.0.2b8 does not.
        // It seems preferable to adopt a simple, rational rule that works
        // the same way in inline links and link reference definitions.)
        // 
        // Spaces, tabs, and up to one line ending is allowed around the destination and
        // title:
        [Test]
        public void InlinesLinks_Example510()
        {
            // Example 510
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link](   /uri
            //       "title"  )
            //
            // Should be rendered as:
            //     <p><a href="/uri" title="title">link</a></p>

            TestParser.TestSpec("[link](   /uri\n  \"title\"  )", "<p><a href=\"/uri\" title=\"title\">link</a></p>", "", context: "Example 510\nSection Inlines / Links\n");
        }

        // But it is not allowed between the link text and the
        // following parenthesis:
        [Test]
        public void InlinesLinks_Example511()
        {
            // Example 511
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link] (/uri)
            //
            // Should be rendered as:
            //     <p>[link] (/uri)</p>

            TestParser.TestSpec("[link] (/uri)", "<p>[link] (/uri)</p>", "", context: "Example 511\nSection Inlines / Links\n");
        }

        // The link text may contain balanced brackets, but not unbalanced ones,
        // unless they are escaped:
        [Test]
        public void InlinesLinks_Example512()
        {
            // Example 512
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link [foo [bar]]](/uri)
            //
            // Should be rendered as:
            //     <p><a href="/uri">link [foo [bar]]</a></p>

            TestParser.TestSpec("[link [foo [bar]]](/uri)", "<p><a href=\"/uri\">link [foo [bar]]</a></p>", "", context: "Example 512\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example513()
        {
            // Example 513
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link] bar](/uri)
            //
            // Should be rendered as:
            //     <p>[link] bar](/uri)</p>

            TestParser.TestSpec("[link] bar](/uri)", "<p>[link] bar](/uri)</p>", "", context: "Example 513\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example514()
        {
            // Example 514
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link [bar](/uri)
            //
            // Should be rendered as:
            //     <p>[link <a href="/uri">bar</a></p>

            TestParser.TestSpec("[link [bar](/uri)", "<p>[link <a href=\"/uri\">bar</a></p>", "", context: "Example 514\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example515()
        {
            // Example 515
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link \[bar](/uri)
            //
            // Should be rendered as:
            //     <p><a href="/uri">link [bar</a></p>

            TestParser.TestSpec("[link \\[bar](/uri)", "<p><a href=\"/uri\">link [bar</a></p>", "", context: "Example 515\nSection Inlines / Links\n");
        }

        // The link text may contain inline content:
        [Test]
        public void InlinesLinks_Example516()
        {
            // Example 516
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link *foo **bar** `#`*](/uri)
            //
            // Should be rendered as:
            //     <p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>

            TestParser.TestSpec("[link *foo **bar** `#`*](/uri)", "<p><a href=\"/uri\">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>", "", context: "Example 516\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example517()
        {
            // Example 517
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [![moon](moon.jpg)](/uri)
            //
            // Should be rendered as:
            //     <p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>

            TestParser.TestSpec("[![moon](moon.jpg)](/uri)", "<p><a href=\"/uri\"><img src=\"moon.jpg\" alt=\"moon\" /></a></p>", "", context: "Example 517\nSection Inlines / Links\n");
        }

        // However, links may not contain other links, at any level of nesting.
        [Test]
        public void InlinesLinks_Example518()
        {
            // Example 518
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo [bar](/uri)](/uri)
            //
            // Should be rendered as:
            //     <p>[foo <a href="/uri">bar</a>](/uri)</p>

            TestParser.TestSpec("[foo [bar](/uri)](/uri)", "<p>[foo <a href=\"/uri\">bar</a>](/uri)</p>", "", context: "Example 518\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example519()
        {
            // Example 519
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo *[bar [baz](/uri)](/uri)*](/uri)
            //
            // Should be rendered as:
            //     <p>[foo <em>[bar <a href="/uri">baz</a>](/uri)</em>](/uri)</p>

            TestParser.TestSpec("[foo *[bar [baz](/uri)](/uri)*](/uri)", "<p>[foo <em>[bar <a href=\"/uri\">baz</a>](/uri)</em>](/uri)</p>", "", context: "Example 519\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example520()
        {
            // Example 520
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     ![[[foo](uri1)](uri2)](uri3)
            //
            // Should be rendered as:
            //     <p><img src="uri3" alt="[foo](uri2)" /></p>

            TestParser.TestSpec("![[[foo](uri1)](uri2)](uri3)", "<p><img src=\"uri3\" alt=\"[foo](uri2)\" /></p>", "", context: "Example 520\nSection Inlines / Links\n");
        }

        // These cases illustrate the precedence of link text grouping over
        // emphasis grouping:
        [Test]
        public void InlinesLinks_Example521()
        {
            // Example 521
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     *[foo*](/uri)
            //
            // Should be rendered as:
            //     <p>*<a href="/uri">foo*</a></p>

            TestParser.TestSpec("*[foo*](/uri)", "<p>*<a href=\"/uri\">foo*</a></p>", "", context: "Example 521\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example522()
        {
            // Example 522
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo *bar](baz*)
            //
            // Should be rendered as:
            //     <p><a href="baz*">foo *bar</a></p>

            TestParser.TestSpec("[foo *bar](baz*)", "<p><a href=\"baz*\">foo *bar</a></p>", "", context: "Example 522\nSection Inlines / Links\n");
        }

        // Note that brackets that *aren't* part of links do not take
        // precedence:
        [Test]
        public void InlinesLinks_Example523()
        {
            // Example 523
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     *foo [bar* baz]
            //
            // Should be rendered as:
            //     <p><em>foo [bar</em> baz]</p>

            TestParser.TestSpec("*foo [bar* baz]", "<p><em>foo [bar</em> baz]</p>", "", context: "Example 523\nSection Inlines / Links\n");
        }

        // These cases illustrate the precedence of HTML tags, code spans,
        // and autolinks over link grouping:
        [Test]
        public void InlinesLinks_Example524()
        {
            // Example 524
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo <bar attr="](baz)">
            //
            // Should be rendered as:
            //     <p>[foo <bar attr="](baz)"></p>

            TestParser.TestSpec("[foo <bar attr=\"](baz)\">", "<p>[foo <bar attr=\"](baz)\"></p>", "", context: "Example 524\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example525()
        {
            // Example 525
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo`](/uri)`
            //
            // Should be rendered as:
            //     <p>[foo<code>](/uri)</code></p>

            TestParser.TestSpec("[foo`](/uri)`", "<p>[foo<code>](/uri)</code></p>", "", context: "Example 525\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example526()
        {
            // Example 526
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo<https://example.com/?search=](uri)>
            //
            // Should be rendered as:
            //     <p>[foo<a href="https://example.com/?search=%5D(uri)">https://example.com/?search=](uri)</a></p>

            TestParser.TestSpec("[foo<https://example.com/?search=](uri)>", "<p>[foo<a href=\"https://example.com/?search=%5D(uri)\">https://example.com/?search=](uri)</a></p>", "", context: "Example 526\nSection Inlines / Links\n");
        }

        // There are three kinds of [reference link](@)s:
        // [full](#full-reference-link), [collapsed](#collapsed-reference-link),
        // and [shortcut](#shortcut-reference-link).
        // 
        // A [full reference link](@)
        // consists of a [link text] immediately followed by a [link label]
        // that [matches] a [link reference definition] elsewhere in the document.
        // 
        // A [link label](@)  begins with a left bracket (`[`) and ends
        // with the first right bracket (`]`) that is not backslash-escaped.
        // Between these brackets there must be at least one character that is not a space,
        // tab, or line ending.
        // Unescaped square bracket characters are not allowed inside the
        // opening and closing square brackets of [link labels].  A link
        // label can have at most 999 characters inside the square
        // brackets.
        // 
        // One label [matches](@)
        // another just in case their normalized forms are equal.  To normalize a
        // label, strip off the opening and closing brackets,
        // perform the *Unicode case fold*, strip leading and trailing
        // spaces, tabs, and line endings, and collapse consecutive internal
        // spaces, tabs, and line endings to a single space.  If there are multiple
        // matching reference link definitions, the one that comes first in the
        // document is used.  (It is desirable in such cases to emit a warning.)
        // 
        // The link's URI and title are provided by the matching [link
        // reference definition].
        // 
        // Here is a simple example:
        [Test]
        public void InlinesLinks_Example527()
        {
            // Example 527
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][bar]
            //     
            //     [bar]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">foo</a></p>

            TestParser.TestSpec("[foo][bar]\n\n[bar]: /url \"title\"", "<p><a href=\"/url\" title=\"title\">foo</a></p>", "", context: "Example 527\nSection Inlines / Links\n");
        }

        // The rules for the [link text] are the same as with
        // [inline links].  Thus:
        // 
        // The link text may contain balanced brackets, but not unbalanced ones,
        // unless they are escaped:
        [Test]
        public void InlinesLinks_Example528()
        {
            // Example 528
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link [foo [bar]]][ref]
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p><a href="/uri">link [foo [bar]]</a></p>

            TestParser.TestSpec("[link [foo [bar]]][ref]\n\n[ref]: /uri", "<p><a href=\"/uri\">link [foo [bar]]</a></p>", "", context: "Example 528\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example529()
        {
            // Example 529
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link \[bar][ref]
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p><a href="/uri">link [bar</a></p>

            TestParser.TestSpec("[link \\[bar][ref]\n\n[ref]: /uri", "<p><a href=\"/uri\">link [bar</a></p>", "", context: "Example 529\nSection Inlines / Links\n");
        }

        // The link text may contain inline content:
        [Test]
        public void InlinesLinks_Example530()
        {
            // Example 530
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [link *foo **bar** `#`*][ref]
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>

            TestParser.TestSpec("[link *foo **bar** `#`*][ref]\n\n[ref]: /uri", "<p><a href=\"/uri\">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>", "", context: "Example 530\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example531()
        {
            // Example 531
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [![moon](moon.jpg)][ref]
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>

            TestParser.TestSpec("[![moon](moon.jpg)][ref]\n\n[ref]: /uri", "<p><a href=\"/uri\"><img src=\"moon.jpg\" alt=\"moon\" /></a></p>", "", context: "Example 531\nSection Inlines / Links\n");
        }

        // However, links may not contain other links, at any level of nesting.
        [Test]
        public void InlinesLinks_Example532()
        {
            // Example 532
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo [bar](/uri)][ref]
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p>[foo <a href="/uri">bar</a>]<a href="/uri">ref</a></p>

            TestParser.TestSpec("[foo [bar](/uri)][ref]\n\n[ref]: /uri", "<p>[foo <a href=\"/uri\">bar</a>]<a href=\"/uri\">ref</a></p>", "", context: "Example 532\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example533()
        {
            // Example 533
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo *bar [baz][ref]*][ref]
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p>[foo <em>bar <a href="/uri">baz</a></em>]<a href="/uri">ref</a></p>

            TestParser.TestSpec("[foo *bar [baz][ref]*][ref]\n\n[ref]: /uri", "<p>[foo <em>bar <a href=\"/uri\">baz</a></em>]<a href=\"/uri\">ref</a></p>", "", context: "Example 533\nSection Inlines / Links\n");
        }

        // (In the examples above, we have two [shortcut reference links]
        // instead of one [full reference link].)
        // 
        // The following cases illustrate the precedence of link text grouping over
        // emphasis grouping:
        [Test]
        public void InlinesLinks_Example534()
        {
            // Example 534
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     *[foo*][ref]
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p>*<a href="/uri">foo*</a></p>

            TestParser.TestSpec("*[foo*][ref]\n\n[ref]: /uri", "<p>*<a href=\"/uri\">foo*</a></p>", "", context: "Example 534\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example535()
        {
            // Example 535
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo *bar][ref]*
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p><a href="/uri">foo *bar</a>*</p>

            TestParser.TestSpec("[foo *bar][ref]*\n\n[ref]: /uri", "<p><a href=\"/uri\">foo *bar</a>*</p>", "", context: "Example 535\nSection Inlines / Links\n");
        }

        // These cases illustrate the precedence of HTML tags, code spans,
        // and autolinks over link grouping:
        [Test]
        public void InlinesLinks_Example536()
        {
            // Example 536
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo <bar attr="][ref]">
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p>[foo <bar attr="][ref]"></p>

            TestParser.TestSpec("[foo <bar attr=\"][ref]\">\n\n[ref]: /uri", "<p>[foo <bar attr=\"][ref]\"></p>", "", context: "Example 536\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example537()
        {
            // Example 537
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo`][ref]`
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p>[foo<code>][ref]</code></p>

            TestParser.TestSpec("[foo`][ref]`\n\n[ref]: /uri", "<p>[foo<code>][ref]</code></p>", "", context: "Example 537\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example538()
        {
            // Example 538
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo<https://example.com/?search=][ref]>
            //     
            //     [ref]: /uri
            //
            // Should be rendered as:
            //     <p>[foo<a href="https://example.com/?search=%5D%5Bref%5D">https://example.com/?search=][ref]</a></p>

            TestParser.TestSpec("[foo<https://example.com/?search=][ref]>\n\n[ref]: /uri", "<p>[foo<a href=\"https://example.com/?search=%5D%5Bref%5D\">https://example.com/?search=][ref]</a></p>", "", context: "Example 538\nSection Inlines / Links\n");
        }

        // Matching is case-insensitive:
        [Test]
        public void InlinesLinks_Example539()
        {
            // Example 539
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][BaR]
            //     
            //     [bar]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">foo</a></p>

            TestParser.TestSpec("[foo][BaR]\n\n[bar]: /url \"title\"", "<p><a href=\"/url\" title=\"title\">foo</a></p>", "", context: "Example 539\nSection Inlines / Links\n");
        }

        // Unicode case fold is used:
        [Test]
        public void InlinesLinks_Example540()
        {
            // Example 540
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [ẞ]
            //     
            //     [SS]: /url
            //
            // Should be rendered as:
            //     <p><a href="/url">ẞ</a></p>

            TestParser.TestSpec("[ẞ]\n\n[SS]: /url", "<p><a href=\"/url\">ẞ</a></p>", "", context: "Example 540\nSection Inlines / Links\n");
        }

        // Consecutive internal spaces, tabs, and line endings are treated as one space for
        // purposes of determining matching:
        [Test]
        public void InlinesLinks_Example541()
        {
            // Example 541
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [Foo
            //       bar]: /url
            //     
            //     [Baz][Foo bar]
            //
            // Should be rendered as:
            //     <p><a href="/url">Baz</a></p>

            TestParser.TestSpec("[Foo\n  bar]: /url\n\n[Baz][Foo bar]", "<p><a href=\"/url\">Baz</a></p>", "", context: "Example 541\nSection Inlines / Links\n");
        }

        // No spaces, tabs, or line endings are allowed between the [link text] and the
        // [link label]:
        [Test]
        public void InlinesLinks_Example542()
        {
            // Example 542
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo] [bar]
            //     
            //     [bar]: /url "title"
            //
            // Should be rendered as:
            //     <p>[foo] <a href="/url" title="title">bar</a></p>

            TestParser.TestSpec("[foo] [bar]\n\n[bar]: /url \"title\"", "<p>[foo] <a href=\"/url\" title=\"title\">bar</a></p>", "", context: "Example 542\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example543()
        {
            // Example 543
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo]
            //     [bar]
            //     
            //     [bar]: /url "title"
            //
            // Should be rendered as:
            //     <p>[foo]
            //     <a href="/url" title="title">bar</a></p>

            TestParser.TestSpec("[foo]\n[bar]\n\n[bar]: /url \"title\"", "<p>[foo]\n<a href=\"/url\" title=\"title\">bar</a></p>", "", context: "Example 543\nSection Inlines / Links\n");
        }

        // This is a departure from John Gruber's original Markdown syntax
        // description, which explicitly allows whitespace between the link
        // text and the link label.  It brings reference links in line with
        // [inline links], which (according to both original Markdown and
        // this spec) cannot have whitespace after the link text.  More
        // importantly, it prevents inadvertent capture of consecutive
        // [shortcut reference links]. If whitespace is allowed between the
        // link text and the link label, then in the following we will have
        // a single reference link, not two shortcut reference links, as
        // intended:
        // 
        // ``` markdown
        // [foo]
        // [bar]
        // 
        // [foo]: /url1
        // [bar]: /url2
        // ```
        // 
        // (Note that [shortcut reference links] were introduced by Gruber
        // himself in a beta version of `Markdown.pl`, but never included
        // in the official syntax description.  Without shortcut reference
        // links, it is harmless to allow space between the link text and
        // link label; but once shortcut references are introduced, it is
        // too dangerous to allow this, as it frequently leads to
        // unintended results.)
        // 
        // When there are multiple matching [link reference definitions],
        // the first is used:
        [Test]
        public void InlinesLinks_Example544()
        {
            // Example 544
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo]: /url1
            //     
            //     [foo]: /url2
            //     
            //     [bar][foo]
            //
            // Should be rendered as:
            //     <p><a href="/url1">bar</a></p>

            TestParser.TestSpec("[foo]: /url1\n\n[foo]: /url2\n\n[bar][foo]", "<p><a href=\"/url1\">bar</a></p>", "", context: "Example 544\nSection Inlines / Links\n");
        }

        // Note that matching is performed on normalized strings, not parsed
        // inline content.  So the following does not match, even though the
        // labels define equivalent inline content:
        [Test]
        public void InlinesLinks_Example545()
        {
            // Example 545
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [bar][foo\!]
            //     
            //     [foo!]: /url
            //
            // Should be rendered as:
            //     <p>[bar][foo!]</p>

            TestParser.TestSpec("[bar][foo\\!]\n\n[foo!]: /url", "<p>[bar][foo!]</p>", "", context: "Example 545\nSection Inlines / Links\n");
        }

        // [Link labels] cannot contain brackets, unless they are
        // backslash-escaped:
        [Test]
        public void InlinesLinks_Example546()
        {
            // Example 546
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][ref[]
            //     
            //     [ref[]: /uri
            //
            // Should be rendered as:
            //     <p>[foo][ref[]</p>
            //     <p>[ref[]: /uri</p>

            TestParser.TestSpec("[foo][ref[]\n\n[ref[]: /uri", "<p>[foo][ref[]</p>\n<p>[ref[]: /uri</p>", "", context: "Example 546\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example547()
        {
            // Example 547
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][ref[bar]]
            //     
            //     [ref[bar]]: /uri
            //
            // Should be rendered as:
            //     <p>[foo][ref[bar]]</p>
            //     <p>[ref[bar]]: /uri</p>

            TestParser.TestSpec("[foo][ref[bar]]\n\n[ref[bar]]: /uri", "<p>[foo][ref[bar]]</p>\n<p>[ref[bar]]: /uri</p>", "", context: "Example 547\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example548()
        {
            // Example 548
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [[[foo]]]
            //     
            //     [[[foo]]]: /url
            //
            // Should be rendered as:
            //     <p>[[[foo]]]</p>
            //     <p>[[[foo]]]: /url</p>

            TestParser.TestSpec("[[[foo]]]\n\n[[[foo]]]: /url", "<p>[[[foo]]]</p>\n<p>[[[foo]]]: /url</p>", "", context: "Example 548\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example549()
        {
            // Example 549
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][ref\[]
            //     
            //     [ref\[]: /uri
            //
            // Should be rendered as:
            //     <p><a href="/uri">foo</a></p>

            TestParser.TestSpec("[foo][ref\\[]\n\n[ref\\[]: /uri", "<p><a href=\"/uri\">foo</a></p>", "", context: "Example 549\nSection Inlines / Links\n");
        }

        // Note that in this example `]` is not backslash-escaped:
        [Test]
        public void InlinesLinks_Example550()
        {
            // Example 550
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [bar\\]: /uri
            //     
            //     [bar\\]
            //
            // Should be rendered as:
            //     <p><a href="/uri">bar\</a></p>

            TestParser.TestSpec("[bar\\\\]: /uri\n\n[bar\\\\]", "<p><a href=\"/uri\">bar\\</a></p>", "", context: "Example 550\nSection Inlines / Links\n");
        }

        // A [link label] must contain at least one character that is not a space, tab, or
        // line ending:
        [Test]
        public void InlinesLinks_Example551()
        {
            // Example 551
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     []
            //     
            //     []: /uri
            //
            // Should be rendered as:
            //     <p>[]</p>
            //     <p>[]: /uri</p>

            TestParser.TestSpec("[]\n\n[]: /uri", "<p>[]</p>\n<p>[]: /uri</p>", "", context: "Example 551\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example552()
        {
            // Example 552
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [
            //      ]
            //     
            //     [
            //      ]: /uri
            //
            // Should be rendered as:
            //     <p>[
            //     ]</p>
            //     <p>[
            //     ]: /uri</p>

            TestParser.TestSpec("[\n ]\n\n[\n ]: /uri", "<p>[\n]</p>\n<p>[\n]: /uri</p>", "", context: "Example 552\nSection Inlines / Links\n");
        }

        // A [collapsed reference link](@)
        // consists of a [link label] that [matches] a
        // [link reference definition] elsewhere in the
        // document, followed by the string `[]`.
        // The contents of the link label are parsed as inlines,
        // which are used as the link's text.  The link's URI and title are
        // provided by the matching reference link definition.  Thus,
        // `[foo][]` is equivalent to `[foo][foo]`.
        [Test]
        public void InlinesLinks_Example553()
        {
            // Example 553
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">foo</a></p>

            TestParser.TestSpec("[foo][]\n\n[foo]: /url \"title\"", "<p><a href=\"/url\" title=\"title\">foo</a></p>", "", context: "Example 553\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example554()
        {
            // Example 554
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [*foo* bar][]
            //     
            //     [*foo* bar]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title"><em>foo</em> bar</a></p>

            TestParser.TestSpec("[*foo* bar][]\n\n[*foo* bar]: /url \"title\"", "<p><a href=\"/url\" title=\"title\"><em>foo</em> bar</a></p>", "", context: "Example 554\nSection Inlines / Links\n");
        }

        // The link labels are case-insensitive:
        [Test]
        public void InlinesLinks_Example555()
        {
            // Example 555
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [Foo][]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">Foo</a></p>

            TestParser.TestSpec("[Foo][]\n\n[foo]: /url \"title\"", "<p><a href=\"/url\" title=\"title\">Foo</a></p>", "", context: "Example 555\nSection Inlines / Links\n");
        }

        // As with full reference links, spaces, tabs, or line endings are not
        // allowed between the two sets of brackets:
        [Test]
        public void InlinesLinks_Example556()
        {
            // Example 556
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo] 
            //     []
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">foo</a>
            //     []</p>

            TestParser.TestSpec("[foo] \n[]\n\n[foo]: /url \"title\"", "<p><a href=\"/url\" title=\"title\">foo</a>\n[]</p>", "", context: "Example 556\nSection Inlines / Links\n");
        }

        // A [shortcut reference link](@)
        // consists of a [link label] that [matches] a
        // [link reference definition] elsewhere in the
        // document and is not followed by `[]` or a link label.
        // The contents of the link label are parsed as inlines,
        // which are used as the link's text.  The link's URI and title
        // are provided by the matching link reference definition.
        // Thus, `[foo]` is equivalent to `[foo][]`.
        [Test]
        public void InlinesLinks_Example557()
        {
            // Example 557
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">foo</a></p>

            TestParser.TestSpec("[foo]\n\n[foo]: /url \"title\"", "<p><a href=\"/url\" title=\"title\">foo</a></p>", "", context: "Example 557\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example558()
        {
            // Example 558
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [*foo* bar]
            //     
            //     [*foo* bar]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title"><em>foo</em> bar</a></p>

            TestParser.TestSpec("[*foo* bar]\n\n[*foo* bar]: /url \"title\"", "<p><a href=\"/url\" title=\"title\"><em>foo</em> bar</a></p>", "", context: "Example 558\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example559()
        {
            // Example 559
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [[*foo* bar]]
            //     
            //     [*foo* bar]: /url "title"
            //
            // Should be rendered as:
            //     <p>[<a href="/url" title="title"><em>foo</em> bar</a>]</p>

            TestParser.TestSpec("[[*foo* bar]]\n\n[*foo* bar]: /url \"title\"", "<p>[<a href=\"/url\" title=\"title\"><em>foo</em> bar</a>]</p>", "", context: "Example 559\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example560()
        {
            // Example 560
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [[bar [foo]
            //     
            //     [foo]: /url
            //
            // Should be rendered as:
            //     <p>[[bar <a href="/url">foo</a></p>

            TestParser.TestSpec("[[bar [foo]\n\n[foo]: /url", "<p>[[bar <a href=\"/url\">foo</a></p>", "", context: "Example 560\nSection Inlines / Links\n");
        }

        // The link labels are case-insensitive:
        [Test]
        public void InlinesLinks_Example561()
        {
            // Example 561
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [Foo]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><a href="/url" title="title">Foo</a></p>

            TestParser.TestSpec("[Foo]\n\n[foo]: /url \"title\"", "<p><a href=\"/url\" title=\"title\">Foo</a></p>", "", context: "Example 561\nSection Inlines / Links\n");
        }

        // A space after the link text should be preserved:
        [Test]
        public void InlinesLinks_Example562()
        {
            // Example 562
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo] bar
            //     
            //     [foo]: /url
            //
            // Should be rendered as:
            //     <p><a href="/url">foo</a> bar</p>

            TestParser.TestSpec("[foo] bar\n\n[foo]: /url", "<p><a href=\"/url\">foo</a> bar</p>", "", context: "Example 562\nSection Inlines / Links\n");
        }

        // If you just want bracketed text, you can backslash-escape the
        // opening bracket to avoid links:
        [Test]
        public void InlinesLinks_Example563()
        {
            // Example 563
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     \[foo]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p>[foo]</p>

            TestParser.TestSpec("\\[foo]\n\n[foo]: /url \"title\"", "<p>[foo]</p>", "", context: "Example 563\nSection Inlines / Links\n");
        }

        // Note that this is a link, because a link label ends with the first
        // following closing bracket:
        [Test]
        public void InlinesLinks_Example564()
        {
            // Example 564
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo*]: /url
            //     
            //     *[foo*]
            //
            // Should be rendered as:
            //     <p>*<a href="/url">foo*</a></p>

            TestParser.TestSpec("[foo*]: /url\n\n*[foo*]", "<p>*<a href=\"/url\">foo*</a></p>", "", context: "Example 564\nSection Inlines / Links\n");
        }

        // Full and collapsed references take precedence over shortcut
        // references:
        [Test]
        public void InlinesLinks_Example565()
        {
            // Example 565
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][bar]
            //     
            //     [foo]: /url1
            //     [bar]: /url2
            //
            // Should be rendered as:
            //     <p><a href="/url2">foo</a></p>

            TestParser.TestSpec("[foo][bar]\n\n[foo]: /url1\n[bar]: /url2", "<p><a href=\"/url2\">foo</a></p>", "", context: "Example 565\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example566()
        {
            // Example 566
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][]
            //     
            //     [foo]: /url1
            //
            // Should be rendered as:
            //     <p><a href="/url1">foo</a></p>

            TestParser.TestSpec("[foo][]\n\n[foo]: /url1", "<p><a href=\"/url1\">foo</a></p>", "", context: "Example 566\nSection Inlines / Links\n");
        }

        // Inline links also take precedence:
        [Test]
        public void InlinesLinks_Example567()
        {
            // Example 567
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo]()
            //     
            //     [foo]: /url1
            //
            // Should be rendered as:
            //     <p><a href="">foo</a></p>

            TestParser.TestSpec("[foo]()\n\n[foo]: /url1", "<p><a href=\"\">foo</a></p>", "", context: "Example 567\nSection Inlines / Links\n");
        }

        [Test]
        public void InlinesLinks_Example568()
        {
            // Example 568
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo](not a link)
            //     
            //     [foo]: /url1
            //
            // Should be rendered as:
            //     <p><a href="/url1">foo</a>(not a link)</p>

            TestParser.TestSpec("[foo](not a link)\n\n[foo]: /url1", "<p><a href=\"/url1\">foo</a>(not a link)</p>", "", context: "Example 568\nSection Inlines / Links\n");
        }

        // In the following case `[bar][baz]` is parsed as a reference,
        // `[foo]` as normal text:
        [Test]
        public void InlinesLinks_Example569()
        {
            // Example 569
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][bar][baz]
            //     
            //     [baz]: /url
            //
            // Should be rendered as:
            //     <p>[foo]<a href="/url">bar</a></p>

            TestParser.TestSpec("[foo][bar][baz]\n\n[baz]: /url", "<p>[foo]<a href=\"/url\">bar</a></p>", "", context: "Example 569\nSection Inlines / Links\n");
        }

        // Here, though, `[foo][bar]` is parsed as a reference, since
        // `[bar]` is defined:
        [Test]
        public void InlinesLinks_Example570()
        {
            // Example 570
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][bar][baz]
            //     
            //     [baz]: /url1
            //     [bar]: /url2
            //
            // Should be rendered as:
            //     <p><a href="/url2">foo</a><a href="/url1">baz</a></p>

            TestParser.TestSpec("[foo][bar][baz]\n\n[baz]: /url1\n[bar]: /url2", "<p><a href=\"/url2\">foo</a><a href=\"/url1\">baz</a></p>", "", context: "Example 570\nSection Inlines / Links\n");
        }

        // Here `[foo]` is not parsed as a shortcut reference, because it
        // is followed by a link label (even though `[bar]` is not defined):
        [Test]
        public void InlinesLinks_Example571()
        {
            // Example 571
            // Section: Inlines / Links
            //
            // The following Markdown:
            //     [foo][bar][baz]
            //     
            //     [baz]: /url1
            //     [foo]: /url2
            //
            // Should be rendered as:
            //     <p>[foo]<a href="/url1">bar</a></p>

            TestParser.TestSpec("[foo][bar][baz]\n\n[baz]: /url1\n[foo]: /url2", "<p>[foo]<a href=\"/url1\">bar</a></p>", "", context: "Example 571\nSection Inlines / Links\n");
        }
    }

    [TestFixture]
    public class TestInlinesImages
    {
        // ## Images
        // 
        // Syntax for images is like the syntax for links, with one
        // difference. Instead of [link text], we have an
        // [image description](@).  The rules for this are the
        // same as for [link text], except that (a) an
        // image description starts with `![` rather than `[`, and
        // (b) an image description may contain links.
        // An image description has inline elements
        // as its contents.  When an image is rendered to HTML,
        // this is standardly used as the image's `alt` attribute.
        [Test]
        public void InlinesImages_Example572()
        {
            // Example 572
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo](/url "title")
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo" title="title" /></p>

            TestParser.TestSpec("![foo](/url \"title\")", "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>", "", context: "Example 572\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example573()
        {
            // Example 573
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo *bar*]
            //     
            //     [foo *bar*]: train.jpg "train & tracks"
            //
            // Should be rendered as:
            //     <p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>

            TestParser.TestSpec("![foo *bar*]\n\n[foo *bar*]: train.jpg \"train & tracks\"", "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>", "", context: "Example 573\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example574()
        {
            // Example 574
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo ![bar](/url)](/url2)
            //
            // Should be rendered as:
            //     <p><img src="/url2" alt="foo bar" /></p>

            TestParser.TestSpec("![foo ![bar](/url)](/url2)", "<p><img src=\"/url2\" alt=\"foo bar\" /></p>", "", context: "Example 574\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example575()
        {
            // Example 575
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo [bar](/url)](/url2)
            //
            // Should be rendered as:
            //     <p><img src="/url2" alt="foo bar" /></p>

            TestParser.TestSpec("![foo [bar](/url)](/url2)", "<p><img src=\"/url2\" alt=\"foo bar\" /></p>", "", context: "Example 575\nSection Inlines / Images\n");
        }

        // Though this spec is concerned with parsing, not rendering, it is
        // recommended that in rendering to HTML, only the plain string content
        // of the [image description] be used.  Note that in
        // the above example, the alt attribute's value is `foo bar`, not `foo
        // [bar](/url)` or `foo <a href="/url">bar</a>`.  Only the plain string
        // content is rendered, without formatting.
        [Test]
        public void InlinesImages_Example576()
        {
            // Example 576
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo *bar*][]
            //     
            //     [foo *bar*]: train.jpg "train & tracks"
            //
            // Should be rendered as:
            //     <p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>

            TestParser.TestSpec("![foo *bar*][]\n\n[foo *bar*]: train.jpg \"train & tracks\"", "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>", "", context: "Example 576\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example577()
        {
            // Example 577
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo *bar*][foobar]
            //     
            //     [FOOBAR]: train.jpg "train & tracks"
            //
            // Should be rendered as:
            //     <p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>

            TestParser.TestSpec("![foo *bar*][foobar]\n\n[FOOBAR]: train.jpg \"train & tracks\"", "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>", "", context: "Example 577\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example578()
        {
            // Example 578
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo](train.jpg)
            //
            // Should be rendered as:
            //     <p><img src="train.jpg" alt="foo" /></p>

            TestParser.TestSpec("![foo](train.jpg)", "<p><img src=\"train.jpg\" alt=\"foo\" /></p>", "", context: "Example 578\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example579()
        {
            // Example 579
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     My ![foo bar](/path/to/train.jpg  "title"   )
            //
            // Should be rendered as:
            //     <p>My <img src="/path/to/train.jpg" alt="foo bar" title="title" /></p>

            TestParser.TestSpec("My ![foo bar](/path/to/train.jpg  \"title\"   )", "<p>My <img src=\"/path/to/train.jpg\" alt=\"foo bar\" title=\"title\" /></p>", "", context: "Example 579\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example580()
        {
            // Example 580
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo](<url>)
            //
            // Should be rendered as:
            //     <p><img src="url" alt="foo" /></p>

            TestParser.TestSpec("![foo](<url>)", "<p><img src=\"url\" alt=\"foo\" /></p>", "", context: "Example 580\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example581()
        {
            // Example 581
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![](/url)
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="" /></p>

            TestParser.TestSpec("![](/url)", "<p><img src=\"/url\" alt=\"\" /></p>", "", context: "Example 581\nSection Inlines / Images\n");
        }

        // Reference-style:
        [Test]
        public void InlinesImages_Example582()
        {
            // Example 582
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo][bar]
            //     
            //     [bar]: /url
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo" /></p>

            TestParser.TestSpec("![foo][bar]\n\n[bar]: /url", "<p><img src=\"/url\" alt=\"foo\" /></p>", "", context: "Example 582\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example583()
        {
            // Example 583
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo][bar]
            //     
            //     [BAR]: /url
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo" /></p>

            TestParser.TestSpec("![foo][bar]\n\n[BAR]: /url", "<p><img src=\"/url\" alt=\"foo\" /></p>", "", context: "Example 583\nSection Inlines / Images\n");
        }

        // Collapsed:
        [Test]
        public void InlinesImages_Example584()
        {
            // Example 584
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo][]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo" title="title" /></p>

            TestParser.TestSpec("![foo][]\n\n[foo]: /url \"title\"", "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>", "", context: "Example 584\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example585()
        {
            // Example 585
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![*foo* bar][]
            //     
            //     [*foo* bar]: /url "title"
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo bar" title="title" /></p>

            TestParser.TestSpec("![*foo* bar][]\n\n[*foo* bar]: /url \"title\"", "<p><img src=\"/url\" alt=\"foo bar\" title=\"title\" /></p>", "", context: "Example 585\nSection Inlines / Images\n");
        }

        // The labels are case-insensitive:
        [Test]
        public void InlinesImages_Example586()
        {
            // Example 586
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![Foo][]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="Foo" title="title" /></p>

            TestParser.TestSpec("![Foo][]\n\n[foo]: /url \"title\"", "<p><img src=\"/url\" alt=\"Foo\" title=\"title\" /></p>", "", context: "Example 586\nSection Inlines / Images\n");
        }

        // As with reference links, spaces, tabs, and line endings, are not allowed
        // between the two sets of brackets:
        [Test]
        public void InlinesImages_Example587()
        {
            // Example 587
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo] 
            //     []
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo" title="title" />
            //     []</p>

            TestParser.TestSpec("![foo] \n[]\n\n[foo]: /url \"title\"", "<p><img src=\"/url\" alt=\"foo\" title=\"title\" />\n[]</p>", "", context: "Example 587\nSection Inlines / Images\n");
        }

        // Shortcut:
        [Test]
        public void InlinesImages_Example588()
        {
            // Example 588
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![foo]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo" title="title" /></p>

            TestParser.TestSpec("![foo]\n\n[foo]: /url \"title\"", "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>", "", context: "Example 588\nSection Inlines / Images\n");
        }

        [Test]
        public void InlinesImages_Example589()
        {
            // Example 589
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![*foo* bar]
            //     
            //     [*foo* bar]: /url "title"
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="foo bar" title="title" /></p>

            TestParser.TestSpec("![*foo* bar]\n\n[*foo* bar]: /url \"title\"", "<p><img src=\"/url\" alt=\"foo bar\" title=\"title\" /></p>", "", context: "Example 589\nSection Inlines / Images\n");
        }

        // Note that link labels cannot contain unescaped brackets:
        [Test]
        public void InlinesImages_Example590()
        {
            // Example 590
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![[foo]]
            //     
            //     [[foo]]: /url "title"
            //
            // Should be rendered as:
            //     <p>![[foo]]</p>
            //     <p>[[foo]]: /url &quot;title&quot;</p>

            TestParser.TestSpec("![[foo]]\n\n[[foo]]: /url \"title\"", "<p>![[foo]]</p>\n<p>[[foo]]: /url &quot;title&quot;</p>", "", context: "Example 590\nSection Inlines / Images\n");
        }

        // The link labels are case-insensitive:
        [Test]
        public void InlinesImages_Example591()
        {
            // Example 591
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     ![Foo]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p><img src="/url" alt="Foo" title="title" /></p>

            TestParser.TestSpec("![Foo]\n\n[foo]: /url \"title\"", "<p><img src=\"/url\" alt=\"Foo\" title=\"title\" /></p>", "", context: "Example 591\nSection Inlines / Images\n");
        }

        // If you just want a literal `!` followed by bracketed text, you can
        // backslash-escape the opening `[`:
        [Test]
        public void InlinesImages_Example592()
        {
            // Example 592
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     !\[foo]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p>![foo]</p>

            TestParser.TestSpec("!\\[foo]\n\n[foo]: /url \"title\"", "<p>![foo]</p>", "", context: "Example 592\nSection Inlines / Images\n");
        }

        // If you want a link after a literal `!`, backslash-escape the
        // `!`:
        [Test]
        public void InlinesImages_Example593()
        {
            // Example 593
            // Section: Inlines / Images
            //
            // The following Markdown:
            //     \![foo]
            //     
            //     [foo]: /url "title"
            //
            // Should be rendered as:
            //     <p>!<a href="/url" title="title">foo</a></p>

            TestParser.TestSpec("\\![foo]\n\n[foo]: /url \"title\"", "<p>!<a href=\"/url\" title=\"title\">foo</a></p>", "", context: "Example 593\nSection Inlines / Images\n");
        }
    }

    [TestFixture]
    public class TestInlinesAutolinks
    {
        // ## Autolinks
        // 
        // [Autolink](@)s are absolute URIs and email addresses inside
        // `<` and `>`. They are parsed as links, with the URL or email address
        // as the link label.
        // 
        // A [URI autolink](@) consists of `<`, followed by an
        // [absolute URI] followed by `>`.  It is parsed as
        // a link to the URI, with the URI as the link's label.
        // 
        // An [absolute URI](@),
        // for these purposes, consists of a [scheme] followed by a colon (`:`)
        // followed by zero or more characters other than [ASCII control
        // characters][ASCII control character], [space], `<`, and `>`.
        // If the URI includes these characters, they must be percent-encoded
        // (e.g. `%20` for a space).
        // 
        // For purposes of this spec, a [scheme](@) is any sequence
        // of 2--32 characters beginning with an ASCII letter and followed
        // by any combination of ASCII letters, digits, or the symbols plus
        // ("+"), period ("."), or hyphen ("-").
        // 
        // Here are some valid autolinks:
        [Test]
        public void InlinesAutolinks_Example594()
        {
            // Example 594
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <http://foo.bar.baz>
            //
            // Should be rendered as:
            //     <p><a href="http://foo.bar.baz">http://foo.bar.baz</a></p>

            TestParser.TestSpec("<http://foo.bar.baz>", "<p><a href=\"http://foo.bar.baz\">http://foo.bar.baz</a></p>", "", context: "Example 594\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example595()
        {
            // Example 595
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <https://foo.bar.baz/test?q=hello&id=22&boolean>
            //
            // Should be rendered as:
            //     <p><a href="https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean">https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>

            TestParser.TestSpec("<https://foo.bar.baz/test?q=hello&id=22&boolean>", "<p><a href=\"https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean\">https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>", "", context: "Example 595\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example596()
        {
            // Example 596
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <irc://foo.bar:2233/baz>
            //
            // Should be rendered as:
            //     <p><a href="irc://foo.bar:2233/baz">irc://foo.bar:2233/baz</a></p>

            TestParser.TestSpec("<irc://foo.bar:2233/baz>", "<p><a href=\"irc://foo.bar:2233/baz\">irc://foo.bar:2233/baz</a></p>", "", context: "Example 596\nSection Inlines / Autolinks\n");
        }

        // Uppercase is also fine:
        [Test]
        public void InlinesAutolinks_Example597()
        {
            // Example 597
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <MAILTO:FOO@BAR.BAZ>
            //
            // Should be rendered as:
            //     <p><a href="MAILTO:FOO@BAR.BAZ">MAILTO:FOO@BAR.BAZ</a></p>

            TestParser.TestSpec("<MAILTO:FOO@BAR.BAZ>", "<p><a href=\"MAILTO:FOO@BAR.BAZ\">MAILTO:FOO@BAR.BAZ</a></p>", "", context: "Example 597\nSection Inlines / Autolinks\n");
        }

        // Note that many strings that count as [absolute URIs] for
        // purposes of this spec are not valid URIs, because their
        // schemes are not registered or because of other problems
        // with their syntax:
        [Test]
        public void InlinesAutolinks_Example598()
        {
            // Example 598
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <a+b+c:d>
            //
            // Should be rendered as:
            //     <p><a href="a+b+c:d">a+b+c:d</a></p>

            TestParser.TestSpec("<a+b+c:d>", "<p><a href=\"a+b+c:d\">a+b+c:d</a></p>", "", context: "Example 598\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example599()
        {
            // Example 599
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <made-up-scheme://foo,bar>
            //
            // Should be rendered as:
            //     <p><a href="made-up-scheme://foo,bar">made-up-scheme://foo,bar</a></p>

            TestParser.TestSpec("<made-up-scheme://foo,bar>", "<p><a href=\"made-up-scheme://foo,bar\">made-up-scheme://foo,bar</a></p>", "", context: "Example 599\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example600()
        {
            // Example 600
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <https://../>
            //
            // Should be rendered as:
            //     <p><a href="https://../">https://../</a></p>

            TestParser.TestSpec("<https://../>", "<p><a href=\"https://../\">https://../</a></p>", "", context: "Example 600\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example601()
        {
            // Example 601
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <localhost:5001/foo>
            //
            // Should be rendered as:
            //     <p><a href="localhost:5001/foo">localhost:5001/foo</a></p>

            TestParser.TestSpec("<localhost:5001/foo>", "<p><a href=\"localhost:5001/foo\">localhost:5001/foo</a></p>", "", context: "Example 601\nSection Inlines / Autolinks\n");
        }

        // Spaces are not allowed in autolinks:
        [Test]
        public void InlinesAutolinks_Example602()
        {
            // Example 602
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <https://foo.bar/baz bim>
            //
            // Should be rendered as:
            //     <p>&lt;https://foo.bar/baz bim&gt;</p>

            TestParser.TestSpec("<https://foo.bar/baz bim>", "<p>&lt;https://foo.bar/baz bim&gt;</p>", "", context: "Example 602\nSection Inlines / Autolinks\n");
        }

        // Backslash-escapes do not work inside autolinks:
        [Test]
        public void InlinesAutolinks_Example603()
        {
            // Example 603
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <https://example.com/\[\>
            //
            // Should be rendered as:
            //     <p><a href="https://example.com/%5C%5B%5C">https://example.com/\[\</a></p>

            TestParser.TestSpec("<https://example.com/\\[\\>", "<p><a href=\"https://example.com/%5C%5B%5C\">https://example.com/\\[\\</a></p>", "", context: "Example 603\nSection Inlines / Autolinks\n");
        }

        // An [email autolink](@)
        // consists of `<`, followed by an [email address],
        // followed by `>`.  The link's label is the email address,
        // and the URL is `mailto:` followed by the email address.
        // 
        // An [email address](@),
        // for these purposes, is anything that matches
        // the [non-normative regex from the HTML5
        // spec](https://html.spec.whatwg.org/multipage/forms.html#e-mail-state-(type=email)):
        // 
        //     /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?
        //     (?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/
        // 
        // Examples of email autolinks:
        [Test]
        public void InlinesAutolinks_Example604()
        {
            // Example 604
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <foo@bar.example.com>
            //
            // Should be rendered as:
            //     <p><a href="mailto:foo@bar.example.com">foo@bar.example.com</a></p>

            TestParser.TestSpec("<foo@bar.example.com>", "<p><a href=\"mailto:foo@bar.example.com\">foo@bar.example.com</a></p>", "", context: "Example 604\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example605()
        {
            // Example 605
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <foo+special@Bar.baz-bar0.com>
            //
            // Should be rendered as:
            //     <p><a href="mailto:foo+special@Bar.baz-bar0.com">foo+special@Bar.baz-bar0.com</a></p>

            TestParser.TestSpec("<foo+special@Bar.baz-bar0.com>", "<p><a href=\"mailto:foo+special@Bar.baz-bar0.com\">foo+special@Bar.baz-bar0.com</a></p>", "", context: "Example 605\nSection Inlines / Autolinks\n");
        }

        // Backslash-escapes do not work inside email autolinks:
        [Test]
        public void InlinesAutolinks_Example606()
        {
            // Example 606
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <foo\+@bar.example.com>
            //
            // Should be rendered as:
            //     <p>&lt;foo+@bar.example.com&gt;</p>

            TestParser.TestSpec("<foo\\+@bar.example.com>", "<p>&lt;foo+@bar.example.com&gt;</p>", "", context: "Example 606\nSection Inlines / Autolinks\n");
        }

        // These are not autolinks:
        [Test]
        public void InlinesAutolinks_Example607()
        {
            // Example 607
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <>
            //
            // Should be rendered as:
            //     <p>&lt;&gt;</p>

            TestParser.TestSpec("<>", "<p>&lt;&gt;</p>", "", context: "Example 607\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example608()
        {
            // Example 608
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     < https://foo.bar >
            //
            // Should be rendered as:
            //     <p>&lt; https://foo.bar &gt;</p>

            TestParser.TestSpec("< https://foo.bar >", "<p>&lt; https://foo.bar &gt;</p>", "", context: "Example 608\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example609()
        {
            // Example 609
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <m:abc>
            //
            // Should be rendered as:
            //     <p>&lt;m:abc&gt;</p>

            TestParser.TestSpec("<m:abc>", "<p>&lt;m:abc&gt;</p>", "", context: "Example 609\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example610()
        {
            // Example 610
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     <foo.bar.baz>
            //
            // Should be rendered as:
            //     <p>&lt;foo.bar.baz&gt;</p>

            TestParser.TestSpec("<foo.bar.baz>", "<p>&lt;foo.bar.baz&gt;</p>", "", context: "Example 610\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example611()
        {
            // Example 611
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     https://example.com
            //
            // Should be rendered as:
            //     <p>https://example.com</p>

            TestParser.TestSpec("https://example.com", "<p>https://example.com</p>", "", context: "Example 611\nSection Inlines / Autolinks\n");
        }

        [Test]
        public void InlinesAutolinks_Example612()
        {
            // Example 612
            // Section: Inlines / Autolinks
            //
            // The following Markdown:
            //     foo@bar.example.com
            //
            // Should be rendered as:
            //     <p>foo@bar.example.com</p>

            TestParser.TestSpec("foo@bar.example.com", "<p>foo@bar.example.com</p>", "", context: "Example 612\nSection Inlines / Autolinks\n");
        }
    }

    [TestFixture]
    public class TestInlinesRawHTML
    {
        // ## Raw HTML
        // 
        // Text between `<` and `>` that looks like an HTML tag is parsed as a
        // raw HTML tag and will be rendered in HTML without escaping.
        // Tag and attribute names are not limited to current HTML tags,
        // so custom tags (and even, say, DocBook tags) may be used.
        // 
        // Here is the grammar for tags:
        // 
        // A [tag name](@) consists of an ASCII letter
        // followed by zero or more ASCII letters, digits, or
        // hyphens (`-`).
        // 
        // An [attribute](@) consists of spaces, tabs, and up to one line ending,
        // an [attribute name], and an optional
        // [attribute value specification].
        // 
        // An [attribute name](@)
        // consists of an ASCII letter, `_`, or `:`, followed by zero or more ASCII
        // letters, digits, `_`, `.`, `:`, or `-`.  (Note:  This is the XML
        // specification restricted to ASCII.  HTML5 is laxer.)
        // 
        // An [attribute value specification](@)
        // consists of optional spaces, tabs, and up to one line ending,
        // a `=` character, optional spaces, tabs, and up to one line ending,
        // and an [attribute value].
        // 
        // An [attribute value](@)
        // consists of an [unquoted attribute value],
        // a [single-quoted attribute value], or a [double-quoted attribute value].
        // 
        // An [unquoted attribute value](@)
        // is a nonempty string of characters not
        // including spaces, tabs, line endings, `"`, `'`, `=`, `<`, `>`, or `` ` ``.
        // 
        // A [single-quoted attribute value](@)
        // consists of `'`, zero or more
        // characters not including `'`, and a final `'`.
        // 
        // A [double-quoted attribute value](@)
        // consists of `"`, zero or more
        // characters not including `"`, and a final `"`.
        // 
        // An [open tag](@) consists of a `<` character, a [tag name],
        // zero or more [attributes], optional spaces, tabs, and up to one line ending,
        // an optional `/` character, and a `>` character.
        // 
        // A [closing tag](@) consists of the string `</`, a
        // [tag name], optional spaces, tabs, and up to one line ending, and the character
        // `>`.
        // 
        // An [HTML comment](@) consists of `<!-->`, `<!--->`, or  `<!--`, a string of
        // characters not including the string `-->`, and `-->` (see the
        // [HTML spec](https://html.spec.whatwg.org/multipage/parsing.html#markup-declaration-open-state)).
        // 
        // A [processing instruction](@)
        // consists of the string `<?`, a string
        // of characters not including the string `?>`, and the string
        // `?>`.
        // 
        // A [declaration](@) consists of the string `<!`, an ASCII letter, zero or more
        // characters not including the character `>`, and the character `>`.
        // 
        // A [CDATA section](@) consists of
        // the string `<![CDATA[`, a string of characters not including the string
        // `]]>`, and the string `]]>`.
        // 
        // An [HTML tag](@) consists of an [open tag], a [closing tag],
        // an [HTML comment], a [processing instruction], a [declaration],
        // or a [CDATA section].
        // 
        // Here are some simple open tags:
        [Test]
        public void InlinesRawHTML_Example613()
        {
            // Example 613
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a><bab><c2c>
            //
            // Should be rendered as:
            //     <p><a><bab><c2c></p>

            TestParser.TestSpec("<a><bab><c2c>", "<p><a><bab><c2c></p>", "", context: "Example 613\nSection Inlines / Raw HTML\n");
        }

        // Empty elements:
        [Test]
        public void InlinesRawHTML_Example614()
        {
            // Example 614
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a/><b2/>
            //
            // Should be rendered as:
            //     <p><a/><b2/></p>

            TestParser.TestSpec("<a/><b2/>", "<p><a/><b2/></p>", "", context: "Example 614\nSection Inlines / Raw HTML\n");
        }

        // Whitespace is allowed:
        [Test]
        public void InlinesRawHTML_Example615()
        {
            // Example 615
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a  /><b2
            //     data="foo" >
            //
            // Should be rendered as:
            //     <p><a  /><b2
            //     data="foo" ></p>

            TestParser.TestSpec("<a  /><b2\ndata=\"foo\" >", "<p><a  /><b2\ndata=\"foo\" ></p>", "", context: "Example 615\nSection Inlines / Raw HTML\n");
        }

        // With attributes:
        [Test]
        public void InlinesRawHTML_Example616()
        {
            // Example 616
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a foo="bar" bam = 'baz <em>"</em>'
            //     _boolean zoop:33=zoop:33 />
            //
            // Should be rendered as:
            //     <p><a foo="bar" bam = 'baz <em>"</em>'
            //     _boolean zoop:33=zoop:33 /></p>

            TestParser.TestSpec("<a foo=\"bar\" bam = 'baz <em>\"</em>'\n_boolean zoop:33=zoop:33 />", "<p><a foo=\"bar\" bam = 'baz <em>\"</em>'\n_boolean zoop:33=zoop:33 /></p>", "", context: "Example 616\nSection Inlines / Raw HTML\n");
        }

        // Custom tag names can be used:
        [Test]
        public void InlinesRawHTML_Example617()
        {
            // Example 617
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     Foo <responsive-image src="foo.jpg" />
            //
            // Should be rendered as:
            //     <p>Foo <responsive-image src="foo.jpg" /></p>

            TestParser.TestSpec("Foo <responsive-image src=\"foo.jpg\" />", "<p>Foo <responsive-image src=\"foo.jpg\" /></p>", "", context: "Example 617\nSection Inlines / Raw HTML\n");
        }

        // Illegal tag names, not parsed as HTML:
        [Test]
        public void InlinesRawHTML_Example618()
        {
            // Example 618
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <33> <__>
            //
            // Should be rendered as:
            //     <p>&lt;33&gt; &lt;__&gt;</p>

            TestParser.TestSpec("<33> <__>", "<p>&lt;33&gt; &lt;__&gt;</p>", "", context: "Example 618\nSection Inlines / Raw HTML\n");
        }

        // Illegal attribute names:
        [Test]
        public void InlinesRawHTML_Example619()
        {
            // Example 619
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a h*#ref="hi">
            //
            // Should be rendered as:
            //     <p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>

            TestParser.TestSpec("<a h*#ref=\"hi\">", "<p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>", "", context: "Example 619\nSection Inlines / Raw HTML\n");
        }

        // Illegal attribute values:
        [Test]
        public void InlinesRawHTML_Example620()
        {
            // Example 620
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a href="hi'> <a href=hi'>
            //
            // Should be rendered as:
            //     <p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>

            TestParser.TestSpec("<a href=\"hi'> <a href=hi'>", "<p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>", "", context: "Example 620\nSection Inlines / Raw HTML\n");
        }

        // Illegal whitespace:
        [Test]
        public void InlinesRawHTML_Example621()
        {
            // Example 621
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     < a><
            //     foo><bar/ >
            //     <foo bar=baz
            //     bim!bop />
            //
            // Should be rendered as:
            //     <p>&lt; a&gt;&lt;
            //     foo&gt;&lt;bar/ &gt;
            //     &lt;foo bar=baz
            //     bim!bop /&gt;</p>

            TestParser.TestSpec("< a><\nfoo><bar/ >\n<foo bar=baz\nbim!bop />", "<p>&lt; a&gt;&lt;\nfoo&gt;&lt;bar/ &gt;\n&lt;foo bar=baz\nbim!bop /&gt;</p>", "", context: "Example 621\nSection Inlines / Raw HTML\n");
        }

        // Missing whitespace:
        [Test]
        public void InlinesRawHTML_Example622()
        {
            // Example 622
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a href='bar'title=title>
            //
            // Should be rendered as:
            //     <p>&lt;a href='bar'title=title&gt;</p>

            TestParser.TestSpec("<a href='bar'title=title>", "<p>&lt;a href='bar'title=title&gt;</p>", "", context: "Example 622\nSection Inlines / Raw HTML\n");
        }

        // Closing tags:
        [Test]
        public void InlinesRawHTML_Example623()
        {
            // Example 623
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     </a></foo >
            //
            // Should be rendered as:
            //     <p></a></foo ></p>

            TestParser.TestSpec("</a></foo >", "<p></a></foo ></p>", "", context: "Example 623\nSection Inlines / Raw HTML\n");
        }

        // Illegal attributes in closing tag:
        [Test]
        public void InlinesRawHTML_Example624()
        {
            // Example 624
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     </a href="foo">
            //
            // Should be rendered as:
            //     <p>&lt;/a href=&quot;foo&quot;&gt;</p>

            TestParser.TestSpec("</a href=\"foo\">", "<p>&lt;/a href=&quot;foo&quot;&gt;</p>", "", context: "Example 624\nSection Inlines / Raw HTML\n");
        }

        // Comments:
        [Test]
        public void InlinesRawHTML_Example625()
        {
            // Example 625
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     foo <!-- this is a --
            //     comment - with hyphens -->
            //
            // Should be rendered as:
            //     <p>foo <!-- this is a --
            //     comment - with hyphens --></p>

            TestParser.TestSpec("foo <!-- this is a --\ncomment - with hyphens -->", "<p>foo <!-- this is a --\ncomment - with hyphens --></p>", "", context: "Example 625\nSection Inlines / Raw HTML\n");
        }

        [Test]
        public void InlinesRawHTML_Example626()
        {
            // Example 626
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     foo <!--> foo -->
            //     
            //     foo <!---> foo -->
            //
            // Should be rendered as:
            //     <p>foo <!--> foo --&gt;</p>
            //     <p>foo <!---> foo --&gt;</p>

            TestParser.TestSpec("foo <!--> foo -->\n\nfoo <!---> foo -->", "<p>foo <!--> foo --&gt;</p>\n<p>foo <!---> foo --&gt;</p>", "", context: "Example 626\nSection Inlines / Raw HTML\n");
        }

        // Processing instructions:
        [Test]
        public void InlinesRawHTML_Example627()
        {
            // Example 627
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     foo <?php echo $a; ?>
            //
            // Should be rendered as:
            //     <p>foo <?php echo $a; ?></p>

            TestParser.TestSpec("foo <?php echo $a; ?>", "<p>foo <?php echo $a; ?></p>", "", context: "Example 627\nSection Inlines / Raw HTML\n");
        }

        // Declarations:
        [Test]
        public void InlinesRawHTML_Example628()
        {
            // Example 628
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     foo <!ELEMENT br EMPTY>
            //
            // Should be rendered as:
            //     <p>foo <!ELEMENT br EMPTY></p>

            TestParser.TestSpec("foo <!ELEMENT br EMPTY>", "<p>foo <!ELEMENT br EMPTY></p>", "", context: "Example 628\nSection Inlines / Raw HTML\n");
        }

        // CDATA sections:
        [Test]
        public void InlinesRawHTML_Example629()
        {
            // Example 629
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     foo <![CDATA[>&<]]>
            //
            // Should be rendered as:
            //     <p>foo <![CDATA[>&<]]></p>

            TestParser.TestSpec("foo <![CDATA[>&<]]>", "<p>foo <![CDATA[>&<]]></p>", "", context: "Example 629\nSection Inlines / Raw HTML\n");
        }

        // Entity and numeric character references are preserved in HTML
        // attributes:
        [Test]
        public void InlinesRawHTML_Example630()
        {
            // Example 630
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     foo <a href="&ouml;">
            //
            // Should be rendered as:
            //     <p>foo <a href="&ouml;"></p>

            TestParser.TestSpec("foo <a href=\"&ouml;\">", "<p>foo <a href=\"&ouml;\"></p>", "", context: "Example 630\nSection Inlines / Raw HTML\n");
        }

        // Backslash escapes do not work in HTML attributes:
        [Test]
        public void InlinesRawHTML_Example631()
        {
            // Example 631
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     foo <a href="\*">
            //
            // Should be rendered as:
            //     <p>foo <a href="\*"></p>

            TestParser.TestSpec("foo <a href=\"\\*\">", "<p>foo <a href=\"\\*\"></p>", "", context: "Example 631\nSection Inlines / Raw HTML\n");
        }

        [Test]
        public void InlinesRawHTML_Example632()
        {
            // Example 632
            // Section: Inlines / Raw HTML
            //
            // The following Markdown:
            //     <a href="\"">
            //
            // Should be rendered as:
            //     <p>&lt;a href=&quot;&quot;&quot;&gt;</p>

            TestParser.TestSpec("<a href=\"\\\"\">", "<p>&lt;a href=&quot;&quot;&quot;&gt;</p>", "", context: "Example 632\nSection Inlines / Raw HTML\n");
        }
    }

    [TestFixture]
    public class TestInlinesHardLineBreaks
    {
        // ## Hard line breaks
        // 
        // A line ending (not in a code span or HTML tag) that is preceded
        // by two or more spaces and does not occur at the end of a block
        // is parsed as a [hard line break](@) (rendered
        // in HTML as a `<br />` tag):
        [Test]
        public void InlinesHardLineBreaks_Example633()
        {
            // Example 633
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     foo  
            //     baz
            //
            // Should be rendered as:
            //     <p>foo<br />
            //     baz</p>

            TestParser.TestSpec("foo  \nbaz", "<p>foo<br />\nbaz</p>", "", context: "Example 633\nSection Inlines / Hard line breaks\n");
        }

        // For a more visible alternative, a backslash before the
        // [line ending] may be used instead of two or more spaces:
        [Test]
        public void InlinesHardLineBreaks_Example634()
        {
            // Example 634
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     foo\
            //     baz
            //
            // Should be rendered as:
            //     <p>foo<br />
            //     baz</p>

            TestParser.TestSpec("foo\\\nbaz", "<p>foo<br />\nbaz</p>", "", context: "Example 634\nSection Inlines / Hard line breaks\n");
        }

        // More than two spaces can be used:
        [Test]
        public void InlinesHardLineBreaks_Example635()
        {
            // Example 635
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     foo       
            //     baz
            //
            // Should be rendered as:
            //     <p>foo<br />
            //     baz</p>

            TestParser.TestSpec("foo       \nbaz", "<p>foo<br />\nbaz</p>", "", context: "Example 635\nSection Inlines / Hard line breaks\n");
        }

        // Leading spaces at the beginning of the next line are ignored:
        [Test]
        public void InlinesHardLineBreaks_Example636()
        {
            // Example 636
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     foo  
            //          bar
            //
            // Should be rendered as:
            //     <p>foo<br />
            //     bar</p>

            TestParser.TestSpec("foo  \n     bar", "<p>foo<br />\nbar</p>", "", context: "Example 636\nSection Inlines / Hard line breaks\n");
        }

        [Test]
        public void InlinesHardLineBreaks_Example637()
        {
            // Example 637
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     foo\
            //          bar
            //
            // Should be rendered as:
            //     <p>foo<br />
            //     bar</p>

            TestParser.TestSpec("foo\\\n     bar", "<p>foo<br />\nbar</p>", "", context: "Example 637\nSection Inlines / Hard line breaks\n");
        }

        // Hard line breaks can occur inside emphasis, links, and other constructs
        // that allow inline content:
        [Test]
        public void InlinesHardLineBreaks_Example638()
        {
            // Example 638
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     *foo  
            //     bar*
            //
            // Should be rendered as:
            //     <p><em>foo<br />
            //     bar</em></p>

            TestParser.TestSpec("*foo  \nbar*", "<p><em>foo<br />\nbar</em></p>", "", context: "Example 638\nSection Inlines / Hard line breaks\n");
        }

        [Test]
        public void InlinesHardLineBreaks_Example639()
        {
            // Example 639
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     *foo\
            //     bar*
            //
            // Should be rendered as:
            //     <p><em>foo<br />
            //     bar</em></p>

            TestParser.TestSpec("*foo\\\nbar*", "<p><em>foo<br />\nbar</em></p>", "", context: "Example 639\nSection Inlines / Hard line breaks\n");
        }

        // Hard line breaks do not occur inside code spans
        [Test]
        public void InlinesHardLineBreaks_Example640()
        {
            // Example 640
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     `code  
            //     span`
            //
            // Should be rendered as:
            //     <p><code>code   span</code></p>

            TestParser.TestSpec("`code  \nspan`", "<p><code>code   span</code></p>", "", context: "Example 640\nSection Inlines / Hard line breaks\n");
        }

        [Test]
        public void InlinesHardLineBreaks_Example641()
        {
            // Example 641
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     `code\
            //     span`
            //
            // Should be rendered as:
            //     <p><code>code\ span</code></p>

            TestParser.TestSpec("`code\\\nspan`", "<p><code>code\\ span</code></p>", "", context: "Example 641\nSection Inlines / Hard line breaks\n");
        }

        // or HTML tags:
        [Test]
        public void InlinesHardLineBreaks_Example642()
        {
            // Example 642
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     <a href="foo  
            //     bar">
            //
            // Should be rendered as:
            //     <p><a href="foo  
            //     bar"></p>

            TestParser.TestSpec("<a href=\"foo  \nbar\">", "<p><a href=\"foo  \nbar\"></p>", "", context: "Example 642\nSection Inlines / Hard line breaks\n");
        }

        [Test]
        public void InlinesHardLineBreaks_Example643()
        {
            // Example 643
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     <a href="foo\
            //     bar">
            //
            // Should be rendered as:
            //     <p><a href="foo\
            //     bar"></p>

            TestParser.TestSpec("<a href=\"foo\\\nbar\">", "<p><a href=\"foo\\\nbar\"></p>", "", context: "Example 643\nSection Inlines / Hard line breaks\n");
        }

        // Hard line breaks are for separating inline content within a block.
        // Neither syntax for hard line breaks works at the end of a paragraph or
        // other block element:
        [Test]
        public void InlinesHardLineBreaks_Example644()
        {
            // Example 644
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     foo\
            //
            // Should be rendered as:
            //     <p>foo\</p>

            TestParser.TestSpec("foo\\", "<p>foo\\</p>", "", context: "Example 644\nSection Inlines / Hard line breaks\n");
        }

        [Test]
        public void InlinesHardLineBreaks_Example645()
        {
            // Example 645
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     foo  
            //
            // Should be rendered as:
            //     <p>foo</p>

            TestParser.TestSpec("foo  ", "<p>foo</p>", "", context: "Example 645\nSection Inlines / Hard line breaks\n");
        }

        [Test]
        public void InlinesHardLineBreaks_Example646()
        {
            // Example 646
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     ### foo\
            //
            // Should be rendered as:
            //     <h3>foo\</h3>

            TestParser.TestSpec("### foo\\", "<h3>foo\\</h3>", "", context: "Example 646\nSection Inlines / Hard line breaks\n");
        }

        [Test]
        public void InlinesHardLineBreaks_Example647()
        {
            // Example 647
            // Section: Inlines / Hard line breaks
            //
            // The following Markdown:
            //     ### foo  
            //
            // Should be rendered as:
            //     <h3>foo</h3>

            TestParser.TestSpec("### foo  ", "<h3>foo</h3>", "", context: "Example 647\nSection Inlines / Hard line breaks\n");
        }
    }

    [TestFixture]
    public class TestInlinesSoftLineBreaks
    {
        // ## Soft line breaks
        // 
        // A regular line ending (not in a code span or HTML tag) that is not
        // preceded by two or more spaces or a backslash is parsed as a
        // [softbreak](@).  (A soft line break may be rendered in HTML either as a
        // [line ending] or as a space. The result will be the same in
        // browsers. In the examples here, a [line ending] will be used.)
        [Test]
        public void InlinesSoftLineBreaks_Example648()
        {
            // Example 648
            // Section: Inlines / Soft line breaks
            //
            // The following Markdown:
            //     foo
            //     baz
            //
            // Should be rendered as:
            //     <p>foo
            //     baz</p>

            TestParser.TestSpec("foo\nbaz", "<p>foo\nbaz</p>", "", context: "Example 648\nSection Inlines / Soft line breaks\n");
        }

        // Spaces at the end of the line and beginning of the next line are
        // removed:
        [Test]
        public void InlinesSoftLineBreaks_Example649()
        {
            // Example 649
            // Section: Inlines / Soft line breaks
            //
            // The following Markdown:
            //     foo 
            //      baz
            //
            // Should be rendered as:
            //     <p>foo
            //     baz</p>

            TestParser.TestSpec("foo \n baz", "<p>foo\nbaz</p>", "", context: "Example 649\nSection Inlines / Soft line breaks\n");
        }
    }

    [TestFixture]
    public class TestInlinesTextualContent
    {
        // A conforming parser may render a soft line break in HTML either as a
        // line ending or as a space.
        // 
        // A renderer may also provide an option to render soft line breaks
        // as hard line breaks.
        // 
        // ## Textual content
        // 
        // Any characters not given an interpretation by the above rules will
        // be parsed as plain textual content.
        [Test]
        public void InlinesTextualContent_Example650()
        {
            // Example 650
            // Section: Inlines / Textual content
            //
            // The following Markdown:
            //     hello $.;'there
            //
            // Should be rendered as:
            //     <p>hello $.;'there</p>

            TestParser.TestSpec("hello $.;'there", "<p>hello $.;'there</p>", "", context: "Example 650\nSection Inlines / Textual content\n");
        }

        [Test]
        public void InlinesTextualContent_Example651()
        {
            // Example 651
            // Section: Inlines / Textual content
            //
            // The following Markdown:
            //     Foo χρῆν
            //
            // Should be rendered as:
            //     <p>Foo χρῆν</p>

            TestParser.TestSpec("Foo χρῆν", "<p>Foo χρῆν</p>", "", context: "Example 651\nSection Inlines / Textual content\n");
        }

        // Internal spaces are preserved verbatim:
        [Test]
        public void InlinesTextualContent_Example652()
        {
            // Example 652
            // Section: Inlines / Textual content
            //
            // The following Markdown:
            //     Multiple     spaces
            //
            // Should be rendered as:
            //     <p>Multiple     spaces</p>

            TestParser.TestSpec("Multiple     spaces", "<p>Multiple     spaces</p>", "", context: "Example 652\nSection Inlines / Textual content\n");
        }
        // <!-- END TESTS -->
        // 
        // # Appendix: A parsing strategy
        // 
        // In this appendix we describe some features of the parsing strategy
        // used in the CommonMark reference implementations.
        // 
        // ## Overview
        // 
        // Parsing has two phases:
        // 
        // 1. In the first phase, lines of input are consumed and the block
        // structure of the document---its division into paragraphs, block quotes,
        // list items, and so on---is constructed.  Text is assigned to these
        // blocks but not parsed. Link reference definitions are parsed and a
        // map of links is constructed.
        // 
        // 2. In the second phase, the raw text contents of paragraphs and headings
        // are parsed into sequences of Markdown inline elements (strings,
        // code spans, links, emphasis, and so on), using the map of link
        // references constructed in phase 1.
        // 
        // At each point in processing, the document is represented as a tree of
        // **blocks**.  The root of the tree is a `document` block.  The `document`
        // may have any number of other blocks as **children**.  These children
        // may, in turn, have other blocks as children.  The last child of a block
        // is normally considered **open**, meaning that subsequent lines of input
        // can alter its contents.  (Blocks that are not open are **closed**.)
        // Here, for example, is a possible document tree, with the open blocks
        // marked by arrows:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //        paragraph
        //          "Lorem ipsum dolor\nsit amet."
        //     -> list (type=bullet tight=true bullet_char=-)
        //          list_item
        //            paragraph
        //              "Qui *quodsi iracundia*"
        //       -> list_item
        //         -> paragraph
        //              "aliquando id"
        // ```
        // 
        // ## Phase 1: block structure
        // 
        // Each line that is processed has an effect on this tree.  The line is
        // analyzed and, depending on its contents, the document may be altered
        // in one or more of the following ways:
        // 
        // 1. One or more open blocks may be closed.
        // 2. One or more new blocks may be created as children of the
        //    last open block.
        // 3. Text may be added to the last (deepest) open block remaining
        //    on the tree.
        // 
        // Once a line has been incorporated into the tree in this way,
        // it can be discarded, so input can be read in a stream.
        // 
        // For each line, we follow this procedure:
        // 
        // 1. First we iterate through the open blocks, starting with the
        // root document, and descending through last children down to the last
        // open block.  Each block imposes a condition that the line must satisfy
        // if the block is to remain open.  For example, a block quote requires a
        // `>` character.  A paragraph requires a non-blank line.
        // In this phase we may match all or just some of the open
        // blocks.  But we cannot close unmatched blocks yet, because we may have a
        // [lazy continuation line].
        // 
        // 2.  Next, after consuming the continuation markers for existing
        // blocks, we look for new block starts (e.g. `>` for a block quote).
        // If we encounter a new block start, we close any blocks unmatched
        // in step 1 before creating the new block as a child of the last
        // matched container block.
        // 
        // 3.  Finally, we look at the remainder of the line (after block
        // markers like `>`, list markers, and indentation have been consumed).
        // This is text that can be incorporated into the last open
        // block (a paragraph, code block, heading, or raw HTML).
        // 
        // Setext headings are formed when we see a line of a paragraph
        // that is a [setext heading underline].
        // 
        // Reference link definitions are detected when a paragraph is closed;
        // the accumulated text lines are parsed to see if they begin with
        // one or more reference link definitions.  Any remainder becomes a
        // normal paragraph.
        // 
        // We can see how this works by considering how the tree above is
        // generated by four lines of Markdown:
        // 
        // ``` markdown
        // > Lorem ipsum dolor
        // sit amet.
        // > - Qui *quodsi iracundia*
        // > - aliquando id
        // ```
        // 
        // At the outset, our document model is just
        // 
        // ``` tree
        // -> document
        // ```
        // 
        // The first line of our text,
        // 
        // ``` markdown
        // > Lorem ipsum dolor
        // ```
        // 
        // causes a `block_quote` block to be created as a child of our
        // open `document` block, and a `paragraph` block as a child of
        // the `block_quote`.  Then the text is added to the last open
        // block, the `paragraph`:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //     -> paragraph
        //          "Lorem ipsum dolor"
        // ```
        // 
        // The next line,
        // 
        // ``` markdown
        // sit amet.
        // ```
        // 
        // is a "lazy continuation" of the open `paragraph`, so it gets added
        // to the paragraph's text:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //     -> paragraph
        //          "Lorem ipsum dolor\nsit amet."
        // ```
        // 
        // The third line,
        // 
        // ``` markdown
        // > - Qui *quodsi iracundia*
        // ```
        // 
        // causes the `paragraph` block to be closed, and a new `list` block
        // opened as a child of the `block_quote`.  A `list_item` is also
        // added as a child of the `list`, and a `paragraph` as a child of
        // the `list_item`.  The text is then added to the new `paragraph`:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //        paragraph
        //          "Lorem ipsum dolor\nsit amet."
        //     -> list (type=bullet tight=true bullet_char=-)
        //       -> list_item
        //         -> paragraph
        //              "Qui *quodsi iracundia*"
        // ```
        // 
        // The fourth line,
        // 
        // ``` markdown
        // > - aliquando id
        // ```
        // 
        // causes the `list_item` (and its child the `paragraph`) to be closed,
        // and a new `list_item` opened up as child of the `list`.  A `paragraph`
        // is added as a child of the new `list_item`, to contain the text.
        // We thus obtain the final tree:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //        paragraph
        //          "Lorem ipsum dolor\nsit amet."
        //     -> list (type=bullet tight=true bullet_char=-)
        //          list_item
        //            paragraph
        //              "Qui *quodsi iracundia*"
        //       -> list_item
        //         -> paragraph
        //              "aliquando id"
        // ```
        // 
        // ## Phase 2: inline structure
        // 
        // Once all of the input has been parsed, all open blocks are closed.
        // 
        // We then "walk the tree," visiting every node, and parse raw
        // string contents of paragraphs and headings as inlines.  At this
        // point we have seen all the link reference definitions, so we can
        // resolve reference links as we go.
        // 
        // ``` tree
        // document
        //   block_quote
        //     paragraph
        //       str "Lorem ipsum dolor"
        //       softbreak
        //       str "sit amet."
        //     list (type=bullet tight=true bullet_char=-)
        //       list_item
        //         paragraph
        //           str "Qui "
        //           emph
        //             str "quodsi iracundia"
        //       list_item
        //         paragraph
        //           str "aliquando id"
        // ```
        // 
        // Notice how the [line ending] in the first paragraph has
        // been parsed as a `softbreak`, and the asterisks in the first list item
        // have become an `emph`.
        // 
        // ### An algorithm for parsing nested emphasis and links
        // 
        // By far the trickiest part of inline parsing is handling emphasis,
        // strong emphasis, links, and images.  This is done using the following
        // algorithm.
        // 
        // When we're parsing inlines and we hit either
        // 
        // - a run of `*` or `_` characters, or
        // - a `[` or `![`
        // 
        // we insert a text node with these symbols as its literal content, and we
        // add a pointer to this text node to the [delimiter stack](@).
        // 
        // The [delimiter stack] is a doubly linked list.  Each
        // element contains a pointer to a text node, plus information about
        // 
        // - the type of delimiter (`[`, `![`, `*`, `_`)
        // - the number of delimiters,
        // - whether the delimiter is "active" (all are active to start), and
        // - whether the delimiter is a potential opener, a potential closer,
        //   or both (which depends on what sort of characters precede
        //   and follow the delimiters).
        // 
        // When we hit a `]` character, we call the *look for link or image*
        // procedure (see below).
        // 
        // When we hit the end of the input, we call the *process emphasis*
        // procedure (see below), with `stack_bottom` = NULL.
        // 
        // #### *look for link or image*
        // 
        // Starting at the top of the delimiter stack, we look backwards
        // through the stack for an opening `[` or `![` delimiter.
        // 
        // - If we don't find one, we return a literal text node `]`.
        // 
        // - If we do find one, but it's not *active*, we remove the inactive
        //   delimiter from the stack, and return a literal text node `]`.
        // 
        // - If we find one and it's active, then we parse ahead to see if
        //   we have an inline link/image, reference link/image, collapsed reference
        //   link/image, or shortcut reference link/image.
        // 
        //   + If we don't, then we remove the opening delimiter from the
        //     delimiter stack and return a literal text node `]`.
        // 
        //   + If we do, then
        // 
        //     * We return a link or image node whose children are the inlines
        //       after the text node pointed to by the opening delimiter.
        // 
        //     * We run *process emphasis* on these inlines, with the `[` opener
        //       as `stack_bottom`.
        // 
        //     * We remove the opening delimiter.
        // 
        //     * If we have a link (and not an image), we also set all
        //       `[` delimiters before the opening delimiter to *inactive*.  (This
        //       will prevent us from getting links within links.)
        // 
        // #### *process emphasis*
        // 
        // Parameter `stack_bottom` sets a lower bound to how far we
        // descend in the [delimiter stack].  If it is NULL, we can
        // go all the way to the bottom.  Otherwise, we stop before
        // visiting `stack_bottom`.
        // 
        // Let `current_position` point to the element on the [delimiter stack]
        // just above `stack_bottom` (or the first element if `stack_bottom`
        // is NULL).
        // 
        // We keep track of the `openers_bottom` for each delimiter
        // type (`*`, `_`), indexed to the length of the closing delimiter run
        // (modulo 3) and to whether the closing delimiter can also be an
        // opener.  Initialize this to `stack_bottom`.
        // 
        // Then we repeat the following until we run out of potential
        // closers:
        // 
        // - Move `current_position` forward in the delimiter stack (if needed)
        //   until we find the first potential closer with delimiter `*` or `_`.
        //   (This will be the potential closer closest
        //   to the beginning of the input -- the first one in parse order.)
        // 
        // - Now, look back in the stack (staying above `stack_bottom` and
        //   the `openers_bottom` for this delimiter type) for the
        //   first matching potential opener ("matching" means same delimiter).
        // 
        // - If one is found:
        // 
        //   + Figure out whether we have emphasis or strong emphasis:
        //     if both closer and opener spans have length >= 2, we have
        //     strong, otherwise regular.
        // 
        //   + Insert an emph or strong emph node accordingly, after
        //     the text node corresponding to the opener.
        // 
        //   + Remove any delimiters between the opener and closer from
        //     the delimiter stack.
        // 
        //   + Remove 1 (for regular emph) or 2 (for strong emph) delimiters
        //     from the opening and closing text nodes.  If they become empty
        //     as a result, remove them and remove the corresponding element
        //     of the delimiter stack.  If the closing node is removed, reset
        //     `current_position` to the next element in the stack.
        // 
        // - If none is found:
        // 
        //   + Set `openers_bottom` to the element before `current_position`.
        //     (We know that there are no openers for this kind of closer up to and
        //     including this point, so this puts a lower bound on future searches.)
        // 
        //   + If the closer at `current_position` is not a potential opener,
        //     remove it from the delimiter stack (since we know it can't
        //     be a closer either).
        // 
        //   + Advance `current_position` to the next element in the stack.
        // 
        // After we're done, we remove all delimiters above `stack_bottom` from the
        // delimiter stack.
    }
}
