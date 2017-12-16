using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenNLP
{
    public static class CommonStrings
    {
        public static string PipeTokenWithSpaces = " | ";
        public static string Pipe = "|";
        public static string ForwardSlash = "/";
        public static string Space = " ";
        public static string Comma = ",";
        public static string EqualSign = "=";
        public static string SINGLEQUOTE = "'";
        public static string QuestionMark = "?";
        public static string ExclamationMark = "!";
        public static string Dash = "-";
        public static string DoubleDash = "--";
        public static string SemiColon = ";";
        public static string ThreeDots = "...";

        public const string Start = "start";
        public const string Continue = "cont";
        public const string Other = "other";

        public static string CacheMemoryLimitMegabytes = "cacheMemoryLimitMegabytes";
        public static string BeamSearchContextCache = "beamSearchContextCache";
        public static string PosContextCache = "posContextCache";
        public static string NameContextCache = "nameContextCache";

        public static string EmptyPrimaryQueue = "Empty PQ";
        public static string LeftBracket = "[";
        public static string RightBracket = "]";
        public const string LeftBrace = "{";
        public const string RightBrace = "}";
        public const string LeftParen = "(";
        public const string RightParen = ")";
        public static string INC = "INC";
        public const string TopNode = "TOP";
        public const string TokenNode = "TK";

        /// <summary>
        /// Prefix for outcomes starting a constituent.
        /// </summary>
        public const string StartPrefix = "S-";

        /// <summary>
        /// Prefix for outcomes continuing a constituent.
        /// </summary>
        public const string ContinuePrefix = "C-";

        /// <summary>
        /// Outcome for token which is not contained in a basal constituent.
        /// </summary>
        public const string OtherOutcome = "O";

        /// <summary>
        /// Outcome used when a constituent is complete.
        /// </summary>
        public const string CompleteOutcome = "c";

        /// <summary>
        /// Outcome used when a constituent is incomplete.
        /// </summary>
        public const string IncompleteOutcome = "i";

        public static string MaximumEntropyModelDirectory = "MaximumEntropyModelDirectory";

        public static string CURWORDEQ = "w=";
        public static string CURWORDFEATUREEQ = "wf=";
        public static string CURWORDANDFEATUREEQ = "w&wf=";
        public static string SUFEQ = "suf=";
        public static string PREEQ = "pre=";
        public static string PEQ = "p=";
        public static string PREVDEFEQ = "pd=";
        public static string TEQ = "t=";
        public static string PPEQ = "pp=";
        public static string TTEQ = "tt=";
        public static string NEQ = "n=";
        public static string NNEQ = "nn=";
        public static string PREVEQ = "po=";
        public static string PREVOWORDEQ = "pow=";
        public static string PREVOWORDFEATURE = "powf=";
        public static string PREVPREVOEQ = "ppo=";
        public static string PREVPREVWORDEQ = "ppw=";
        public static string PREVPREVWORDFEATUREEQ = "ppwf=";
        public static string PREVPREVWORDANDFEATUREEQ = "ppw&f=";
        public static string PREVWORDEQ = "pw=";
        public static string PREVWORDFEATUREEQ = "pwf=";
        public static string PREVWORDANDFEATUREEQ = "pw&f=";
        public static string PREVWORDBOS = "pw=BOS";
        public static string PREVPREVWORDBOS = "ppw=BOS";

        public static string COMMACURWORDEQ = ",w=";
        public static string COMMAWORDFEATUREEQ = ",wf=";
        public static string NEXTWORDEQ = "nw=";
        public static string NEXTWORDFEATUREEQ = "nwf=";
        public static string NEXTWORDANDFEATUREEQ = "nw&f=";
        public static string COMMANEXTWORDEQ = ",nw=";
        public static string COMMANEXTWORDFEATUREEQ = ",nwf=";
        public static string NEXTWORDEOS = "nw=EOS";
        public static string COMMANEXTWORDEQEOS = ",nw=EOS";
        public static string NEXTNEXTWORDEOS = "nnw=EOS";
        public static string NEXTNEXTWORDEQ = "nnw=";
        public static string NEXTNEXTWORDFEATUREEQ = "nnwf=";
        public static string NEXTNEXTWORDANDFEATUREEQ = "nnw&f=";

        public static string LOWER_H = "h";
        public static string LOWER_C = "c";
        public static string LOWER_D = "d";
        public static string DEF = "def";

        public static string FEATURELOWERCASE = "lc";
        public static string FEATURE2DIGITS = "2d";
        public static string FEATURE4DIGITS = "4d";
        public static string FEATURENUM = "num";
        public static string FEATURENUMLETTERPATTERN = "an";
        public static string FEATURENUMHYPHENPATTERN = "dd";
        public static string FEATURENUMBACKSLASHPATTERN = "ds";
        public static string FEATURENUMCOMMAPATTERN = "dc";
        public static string FEATURENUMPERIODPATTERN = "dp";
        public static string FEATUREALLCAPSWORDPATTERN = "sc";
        public static string FEATUREALLCAPSPATTERN = "ac";
        public static string FEATURECAPPERIODPATTERN = "cp";
        public static string FEATUREINITIALCAPPATTERN = "ic";
        public static string FEATUREOTHER = "other";
        public static string FEATUREDEFAULT = "default";
        public static string STAREQ = "*=";

        public static string PART_START = "<START>";
        public static string PART_END = "<END>";

        public static string ENDOFSENTENCE = "endofsentence";
        public static string ISCOMMA = "iscomma";
        public static string QUOTESMATCH = "quotesmatch";
        public static string BRACKETSMATCH = "bracketsmatch";

        public static string CURPARSEWORDLCB = "-LCB-";
        public static string CURPARSEWORDRCB = "-RCB-";
        public static string CURPARSEWORDLRB = "-LRB-";
        public static string CURPARSEWORDRRB = "-RRB-";
    }

    public static class CommonFiles
    {
        public static string EnglishSD_nbin = "EnglishSD.nbin";
        public static string EnglishPOS_nbin = "EnglishPOS.nbin";
        public static string EnglishChunk_nbin = "EnglishChunk.nbin";

    }

    public static class CommonDirectories
    {
        public static string Parser_TagDict = "\\Parser\\tagdict";
        public static string NameFindDir = "namefind\\";
        public static string CorefGenDir = "coref\\gen";
        public static string CorefSimDir = "coref\\sim";
        public static string ModelsDir = "../../../Resources/Models/";
        public static string ParserTagDictDir = "parser\\tagdict";
        public static string ParserHeadRulesDir = "parser\\head_rules";
    }

    public static class CommonModels
    {
        public static string Date = "date";
        public static string Location = "location";
        public static string Money = "money";
        public static string Organization = "organization";
        public static string Percentage = "percentage";
        public static string Person = "person";
        public static string Time = "time";
        public static string ModelExtension = ".nbin";
        public static string TreebankFileExtension ="mrg";

        public static string ParserNbuildBinary = "parser\\build.nbin";
        public static string ParserCheckBinary = "parser\\check.nbin";
        public static string ParserTagBinary = "parser\\tag.nbin";
        public static string ParserChunkBinary = "parser\\chunk.nbin";
        public static string ParserEnglishTokBinary = "parser\\EnglishTok.nbin";
    }

    public static class CommonLinkers
    {
        public static string Coref = "coref";

    }

    public static class CommonErrorMessages
    {
        public static string GisModel_GetAllOutcomes_Error = "The double array sent as a parameter to GisModel.GetAllOutcomes() must not have been produced by this model.";
        public static string ConstituentNotInSentence = "Inserting constituent not contained in the sentence!";
        public static string ParseExceptionWrongBaseCase = "Parse.GetTagSequenceProbability(): Wrong base case!";
        public static string ParseObjectMissing = "A Parse object is required for comparison.";
    }

    public static class CommonRegEx
    {
        public static readonly Regex TypePattern = new Regex("^([^ =-]+)", RegexOptions.Compiled);

        /// <summary>   The pattern used to identify tokens in Penn Treebank labeled constituents. </summary>
        public static readonly Regex TokenPattern = new Regex("^[^ ()]+ ([^ ()]+)\\s*\\)", RegexOptions.Compiled);
    }


    public static class CommonAnnotationChars
    {
        public static char Dash = '-';
        public static char Equals = '=';
        public static char Pipe = '|';
        public static char HashTag = '#';
        public static char Hat ='^';
        public static char Tilde = '~';
        public static char Underscore = '_';
        public static char LeftBracket = '[';
    }

    public static class PartsOfSpeechStrings
    {
        // List of all parts of speech ---------------------------------------

        // verbs
        public const string VerbBaseForm = "VB";
        public const string VerbNon3rdPersSingPresent = "VBP";
        public const string Verb3rdPersSingPresent = "VBZ";
        public const string VerbPastTense = "VBD";
        public const string VerbGerundOrPresentParticiple = "VBG";
        public const string VerbPastParticiple = "VBN";

        // adjectives
        public const string Adjective = "JJ";
        public const string AdjectiveComparative = "JJR";
        public const string AdjectiveSuperlative = "JJS";

        // nouns
        public const string NounSingularOrMass = "NN";
        public const string NounPlural = "NNS";
        public const string ProperNounSingular = "NNP";
        public const string ProperNounPlural = "NNPS";

        // adverbs
        public const string WhAdverb = "WRB";
        public const string Adverb = "RB";
        public const string AdverbComparative = "RBR";
        public const string AdverbSuperlative = "RBS";

        // conjunctions
        public const string CoordinatingConjunction = "CC";
        public const string PrepositionOrSubordinateConjunction = "IN";

        // pronouns
        public const string WhPronoun = "WP";
        public const string PossessiveWhPronoun = "WP$";
        public const string PersonalPronoun = "PRP";
        public const string PossessivePronoun = "PRP$";

        // misc
        public const string Particle = "RP";
        public const string CardinalNumber = "CD";
        public const string Determiner = "DT";
        public const string To = "TO";
        public const string ExistentialThere = "EX";
        public const string Interjection = "UH";
        public const string ForeignWord = "FW";
        public const string ListItemMarker = "LS";
        public const string Modal = "MD";
        public const string WhDeterminer = "WDT";
        public const string Predeterminer = "PDT";

        // punctuation
        public const string LeftOpenDoubleQuote = "``";

        public const string PossessiveEnding = "POS";
        public const string Comma = ",";
        public const string RightCloseDoubleQuote = "''";
        public const string SentenceFinalPunctuation = ".";
        public const string ColonSemiColon = ":";
        public const string LeftParenthesis = "-LRB";
        public const string RightParenthesis = "-RRB";

        public const string PunctLeftParenthesis = "-LRB-";
        public const string PunctRightParenthesis = "-RRB-";
        public const string NONE = "-NONE-";
        public const string LCB = "-LCB-";
        public const string RCB = "-RCB-";



        // symbols
        public const string DollarSign = "$";
        public const string PoundSign = "#";
        public const string Symbol = "SYM";

        // sentence ending and beginning
        public const string SentenceEnd = "*SE*";
        public const string SentenceBeginning = "*SB*";

    }

}
