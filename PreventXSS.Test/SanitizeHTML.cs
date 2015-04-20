using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PreventXSS.Test
{
    [TestClass]
    public class SanitizeHTML
    {
        [TestMethod]
        public void Wellformed()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b", "em", "strong" };
            string input = " b>hello</b><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual(" b>hello</b>&ltscript&gtevil&lt/script&gt", result);
        }

        #region "Malformed String"

        //MALFORMED
        // ' b>test<b>
        //  <b> b </b
        // < b > test < /b  >
        // <<b>>


        [TestMethod]
        public void Malformed_2()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "<b>hello /b ><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("<b>hello /b >&ltscript&gtevil&lt/script&gt", result);
        }
        [TestMethod]
        public void Malformed_3()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "< b >hello /b ><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("< b >hello /b >&ltscript&gtevil&lt/script&gt", result);
        }

        [TestMethod]
        public void Malformed_4()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "<<b>><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&lt&gt;b&gt>&ltscript&gtevil&lt/script&gt", result);
        }
        #endregion

        #region "Shortcircuiting conditions"

        // Shortcuts
        //  list - null
        //  list - blank
        //  string - null
        //  strimg - empty
        //  string - no tags in

        [TestMethod]
        public void Shortcuts_1()
        {
            // Arrange
            List<string> validHtmlTags = null;
            string input = "<b>hello</b><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&lt;b&gt;hello&lt;/b&gt;&lt;script&gt;evil&lt;/script&gt;", result);
        }

        [TestMethod]
        public void Shortcuts_2()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { };
            string input = "<b>hello</b><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&lt;b&gt;hello&lt;/b&gt;&lt;script&gt;evil&lt;/script&gt;", result);
        }

        [TestMethod]
        public void Shortcuts_3()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = null;

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void Shortcuts_4()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Shortcuts_5()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "no tags";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("no tags", result);
        }

        [TestMethod]
        public void Shortcuts_6()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "\t\n";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("\t\n", result);
        }

        [TestMethod]
        public void Shortcuts_7()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "      ";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("      ", result);
        }
        #endregion

        #region "Tests for whitelist conditions"

        //list
        // single element
        // multiple elements
        [TestMethod]
        public void list_1()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "<em><b>hello</b></em><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&ltem&gt<b>hello</b>&lt/em&gt&ltscript&gtevil&lt/script&gt", result);
        }
        [TestMethod]
        public void list_2()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b", "em" };
            string input = "<em><b>hello</b></em><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("<em><b>hello</b></em>&ltscript&gtevil&lt/script&gt", result);
        }
        #endregion

        #region "Tests for tag position in string"
        //Position in string
        // first
        // middle 
        // last
        // unbalanced
        [TestMethod]
        public void position_1()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "<b>hello</b><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("<b>hello</b>&ltscript&gtevil&lt/script&gt", result);
        }

        [TestMethod]
        public void position_2()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "<script>evil</script><b>hello</b>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&ltscript&gtevil&lt/script&gt<b>hello</b>", result);
        }
        #endregion

        #region "Case Sensitivity"

        // Case sensitivity
        [TestMethod]
        public void case_1()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "b" };
            string input = "<script>evil</script><B>hello</b>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&ltscript&gtevil&lt/script&gt<B>hello</b>", result);
        }

        // Case sensitivity
        [TestMethod]
        public void case_Mixed()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "em" };
            string input = "<script>evil</script><EM>hello</eM>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&ltscript&gtevil&lt/script&gt<EM>hello</eM>", result);
        }

        // Case sensitivity
        [TestMethod]
        public void case_Mixed2()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "EM" };
            string input = "<script>evil</script><EM>hello</eM>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&ltscript&gtevil&lt/script&gt<EM>hello</eM>", result);
        }
        #endregion

        #region "Whitespace sensitivity"

        // Additional Whitspace in tags meaning invalid for tags
        [TestMethod]
        public void case_Whitespace()
        {
            // Arrange
            List<string> validHtmlTags = new List<string> { "em" };
            string input = "<script>evil</script><  E  M>hello</ eM>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags);

            // Assert
            Assert.AreEqual("&ltscript&gtevil&lt/script&gt&lt  E  M&gthello&lt/ eM&gt", result);
        }
        #endregion

        #region "Calling using Array Overloads"

        // Array parameter
        // With values
        // null array

        [TestMethod]
        public void PassArray()
        {
            // Arrange
            string[] validHtmlTags1 = { "b", "em", "strong" };
            string input = " b>hello</b><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags1);

            // Assert
            Assert.AreEqual(" b>hello</b>&ltscript&gtevil&lt/script&gt", result);
        }

        [TestMethod]
        public void PassArrayBlank()
        {
            // Arrange
            string[] validHtmlTags1 = null;
            string input = "<b>hello</b><script>evil</script>";

            // Act
            string result = SanitizeHTML_Core.SanitizeHTML(input, validHtmlTags1);

            // Assert
            Assert.AreEqual("&lt;b&gt;hello&lt;/b&gt;&lt;script&gt;evil&lt;/script&gt;", result);
        }
        #endregion
    }
}

