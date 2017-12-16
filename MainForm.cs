using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using OpenNLP.Tools.Chunker;
using OpenNLP.Tools.Coreference.Similarity;
using OpenNLP.Tools.Lang.English;
using OpenNLP.Tools.NameFind;
using OpenNLP.Tools.Parser;
using OpenNLP.Tools.PosTagger;
using OpenNLP.Tools.SentenceDetect;
using OpenNLP.Tools.Tokenize;
using OpenNLP;
using OpenNLP.Common;

namespace ToolsExample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : Form
	{
		private Button _btnParse;
		private Button _btnPosTag;
		private Button _btnChunk;
		private Button _btnTokenize;
		private Button _btnNameFind;
        private Button _btnGender;
        private Button _btnSimilarity;
        private Button _btnCoreference;
		private Button _btnSplit;
		private TextBox _txtIn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private readonly Container _components = null;

		private readonly string _modelPath;

		private MaximumEntropySentenceDetector _sentenceDetector;
		private AbstractTokenizer _tokenizer;
		private EnglishMaximumEntropyPosTagger _posTagger;
		private EnglishTreebankChunker _chunker;
        private EnglishTreebankParser _parser;
		private EnglishNameFinder _nameFinder;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Label lblWRB;
        private Label label93;
        private Label lblWPDOLLAR;
        private Label label95;
        private Label lblWDT;
        private Label label97;
        private Label lblVBZ;
        private Label label99;
        private Label lblVBP;
        private Label label101;
        private Label lblVBN;
        private Label label103;
        private Label lblVBG;
        private Label label105;
        private Label lblVBD;
        private Label label107;
        private Label lblSYM;
        private Label label37;
        private Label lblRBS;
        private Label label39;
        private Label lblRBR;
        private Label label41;
        private Label lblPRPDOLLAR;
        private Label label43;
        private Label lblPRP;
        private Label label45;
        private Label lblPOS;
        private Label label47;
        private Label lblPDT;
        private Label label49;
        private Label lblNNPS;
        private Label label51;
        private Label lblNNP;
        private Label label53;
        private Label lblNNS;
        private Label label55;
        private Label lblJJS;
        private Label label57;
        private Label lblJJR;
        private Label label59;
        private Label lblWP;
        private Label label29;
        private Label lblVB;
        private Label label31;
        private Label lblUH;
        private Label label33;
        private Label lblTO;
        private Label label35;
        private Label lblRP;
        private Label label13;
        private Label lblRB;
        private Label label15;
        private Label lblNN;
        private Label label17;
        private Label lblMD;
        private Label label19;
        private Label lblLS;
        private Label label21;
        private Label lblJJ;
        private Label label23;
        private Label lblIN;
        private Label label11;
        private Label lblFW;
        private Label label9;
        private Label lblEX;
        private Label label7;
        private Label lblDT;
        private Label label5;
        private Label lblCD;
        private Label label3;
        private Label lblCC;
        private Label label1;
        private TreebankLinker _coreferenceFinder;
        private ComboBox cboFind;
        private Label label2;
        private Button btnFind;
        private RichTextBox _txtOut;
        private Dictionary<string, long> POSTagDict = new Dictionary<string, long>();



        private MainForm()
		{
			InitializeComponent();

		    LoadPOSTags();
            _modelPath = @"C:\Development\Other\AI\NLP\Models\\";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
			    _components?.Dispose();
			}
			base.Dispose( disposing );
		}

		// Windows Form Designer generated code ------------------

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this._btnParse = new System.Windows.Forms.Button();
            this._btnPosTag = new System.Windows.Forms.Button();
            this._btnChunk = new System.Windows.Forms.Button();
            this._btnTokenize = new System.Windows.Forms.Button();
            this._btnNameFind = new System.Windows.Forms.Button();
            this._btnSplit = new System.Windows.Forms.Button();
            this._txtIn = new System.Windows.Forms.TextBox();
            this._btnGender = new System.Windows.Forms.Button();
            this._btnSimilarity = new System.Windows.Forms.Button();
            this._btnCoreference = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblWRB = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.lblWPDOLLAR = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.lblWDT = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.lblVBZ = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.lblVBP = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.lblVBN = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.lblVBG = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.lblVBD = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.lblSYM = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblRBS = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.lblRBR = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblPRPDOLLAR = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lblPRP = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblPOS = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblPDT = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.lblNNPS = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblNNP = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lblNNS = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.lblJJS = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.lblJJR = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.lblWP = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblVB = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblUH = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblTO = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblRP = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblRB = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblNN = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblMD = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblLS = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblJJ = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblIN = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblFW = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEX = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDT = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCD = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCC = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboFind = new System.Windows.Forms.ComboBox();
            this._txtOut = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnParse
            // 
            this._btnParse.Location = new System.Drawing.Point(356, 146);
            this._btnParse.Name = "_btnParse";
            this._btnParse.Size = new System.Drawing.Size(75, 23);
            this._btnParse.TabIndex = 21;
            this._btnParse.Text = "Parse";
            this._btnParse.Click += new System.EventHandler(this._btnParse_Click);
            // 
            // _btnPosTag
            // 
            this._btnPosTag.Location = new System.Drawing.Point(180, 146);
            this._btnPosTag.Name = "_btnPosTag";
            this._btnPosTag.Size = new System.Drawing.Size(75, 23);
            this._btnPosTag.TabIndex = 20;
            this._btnPosTag.Text = "POS tag";
            this._btnPosTag.Click += new System.EventHandler(this._btnPosTag_Click);
            // 
            // _btnChunk
            // 
            this._btnChunk.Location = new System.Drawing.Point(268, 146);
            this._btnChunk.Name = "_btnChunk";
            this._btnChunk.Size = new System.Drawing.Size(75, 23);
            this._btnChunk.TabIndex = 19;
            this._btnChunk.Text = "Chunk";
            this._btnChunk.Click += new System.EventHandler(this._btnChunk_Click);
            // 
            // _btnTokenize
            // 
            this._btnTokenize.Location = new System.Drawing.Point(92, 146);
            this._btnTokenize.Name = "_btnTokenize";
            this._btnTokenize.Size = new System.Drawing.Size(75, 23);
            this._btnTokenize.TabIndex = 18;
            this._btnTokenize.Text = "Tokenize";
            this._btnTokenize.Click += new System.EventHandler(this._btnTokenize_Click);
            // 
            // _btnNameFind
            // 
            this._btnNameFind.Location = new System.Drawing.Point(444, 146);
            this._btnNameFind.Name = "_btnNameFind";
            this._btnNameFind.Size = new System.Drawing.Size(75, 23);
            this._btnNameFind.TabIndex = 16;
            this._btnNameFind.Text = "Find Names";
            this._btnNameFind.Click += new System.EventHandler(this._btnNameFind_Click);
            // 
            // _btnSplit
            // 
            this._btnSplit.Location = new System.Drawing.Point(4, 146);
            this._btnSplit.Name = "_btnSplit";
            this._btnSplit.Size = new System.Drawing.Size(75, 23);
            this._btnSplit.TabIndex = 14;
            this._btnSplit.Text = "Split";
            this._btnSplit.Click += new System.EventHandler(this._btnSplit_Click);
            // 
            // _txtIn
            // 
            this._txtIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtIn.Location = new System.Drawing.Point(4, 0);
            this._txtIn.Multiline = true;
            this._txtIn.Name = "_txtIn";
            this._txtIn.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtIn.Size = new System.Drawing.Size(794, 140);
            this._txtIn.TabIndex = 13;
            // 
            // _btnGender
            // 
            this._btnGender.Location = new System.Drawing.Point(535, 146);
            this._btnGender.Name = "_btnGender";
            this._btnGender.Size = new System.Drawing.Size(75, 23);
            this._btnGender.TabIndex = 22;
            this._btnGender.Text = "Gender";
            this._btnGender.Click += new System.EventHandler(this._btnGender_Click);
            // 
            // _btnSimilarity
            // 
            this._btnSimilarity.Location = new System.Drawing.Point(623, 146);
            this._btnSimilarity.Name = "_btnSimilarity";
            this._btnSimilarity.Size = new System.Drawing.Size(75, 23);
            this._btnSimilarity.TabIndex = 23;
            this._btnSimilarity.Text = "Similarity";
            this._btnSimilarity.Click += new System.EventHandler(this._btnSimilarity_Click);
            // 
            // _btnCoreference
            // 
            this._btnCoreference.Location = new System.Drawing.Point(713, 146);
            this._btnCoreference.Name = "_btnCoreference";
            this._btnCoreference.Size = new System.Drawing.Size(75, 23);
            this._btnCoreference.TabIndex = 24;
            this._btnCoreference.Text = "Coreference";
            this._btnCoreference.Click += new System.EventHandler(this._btnCoreference_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cboFind);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnFind);
            this.splitContainer1.Panel2.Controls.Add(this.lblWRB);
            this.splitContainer1.Panel2.Controls.Add(this.label93);
            this.splitContainer1.Panel2.Controls.Add(this.lblWPDOLLAR);
            this.splitContainer1.Panel2.Controls.Add(this.label95);
            this.splitContainer1.Panel2.Controls.Add(this.lblWDT);
            this.splitContainer1.Panel2.Controls.Add(this.label97);
            this.splitContainer1.Panel2.Controls.Add(this.lblVBZ);
            this.splitContainer1.Panel2.Controls.Add(this.label99);
            this.splitContainer1.Panel2.Controls.Add(this.lblVBP);
            this.splitContainer1.Panel2.Controls.Add(this.label101);
            this.splitContainer1.Panel2.Controls.Add(this.lblVBN);
            this.splitContainer1.Panel2.Controls.Add(this.label103);
            this.splitContainer1.Panel2.Controls.Add(this.lblVBG);
            this.splitContainer1.Panel2.Controls.Add(this.label105);
            this.splitContainer1.Panel2.Controls.Add(this.lblVBD);
            this.splitContainer1.Panel2.Controls.Add(this.label107);
            this.splitContainer1.Panel2.Controls.Add(this.lblSYM);
            this.splitContainer1.Panel2.Controls.Add(this.label37);
            this.splitContainer1.Panel2.Controls.Add(this.lblRBS);
            this.splitContainer1.Panel2.Controls.Add(this.label39);
            this.splitContainer1.Panel2.Controls.Add(this.lblRBR);
            this.splitContainer1.Panel2.Controls.Add(this.label41);
            this.splitContainer1.Panel2.Controls.Add(this.lblPRPDOLLAR);
            this.splitContainer1.Panel2.Controls.Add(this.label43);
            this.splitContainer1.Panel2.Controls.Add(this.lblPRP);
            this.splitContainer1.Panel2.Controls.Add(this.label45);
            this.splitContainer1.Panel2.Controls.Add(this.lblPOS);
            this.splitContainer1.Panel2.Controls.Add(this.label47);
            this.splitContainer1.Panel2.Controls.Add(this.lblPDT);
            this.splitContainer1.Panel2.Controls.Add(this.label49);
            this.splitContainer1.Panel2.Controls.Add(this.lblNNPS);
            this.splitContainer1.Panel2.Controls.Add(this.label51);
            this.splitContainer1.Panel2.Controls.Add(this.lblNNP);
            this.splitContainer1.Panel2.Controls.Add(this.label53);
            this.splitContainer1.Panel2.Controls.Add(this.lblNNS);
            this.splitContainer1.Panel2.Controls.Add(this.label55);
            this.splitContainer1.Panel2.Controls.Add(this.lblJJS);
            this.splitContainer1.Panel2.Controls.Add(this.label57);
            this.splitContainer1.Panel2.Controls.Add(this.lblJJR);
            this.splitContainer1.Panel2.Controls.Add(this.label59);
            this.splitContainer1.Panel2.Controls.Add(this.lblWP);
            this.splitContainer1.Panel2.Controls.Add(this.label29);
            this.splitContainer1.Panel2.Controls.Add(this.lblVB);
            this.splitContainer1.Panel2.Controls.Add(this.label31);
            this.splitContainer1.Panel2.Controls.Add(this.lblUH);
            this.splitContainer1.Panel2.Controls.Add(this.label33);
            this.splitContainer1.Panel2.Controls.Add(this.lblTO);
            this.splitContainer1.Panel2.Controls.Add(this.label35);
            this.splitContainer1.Panel2.Controls.Add(this.lblRP);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.lblRB);
            this.splitContainer1.Panel2.Controls.Add(this.label15);
            this.splitContainer1.Panel2.Controls.Add(this.lblNN);
            this.splitContainer1.Panel2.Controls.Add(this.label17);
            this.splitContainer1.Panel2.Controls.Add(this.lblMD);
            this.splitContainer1.Panel2.Controls.Add(this.label19);
            this.splitContainer1.Panel2.Controls.Add(this.lblLS);
            this.splitContainer1.Panel2.Controls.Add(this.label21);
            this.splitContainer1.Panel2.Controls.Add(this.lblJJ);
            this.splitContainer1.Panel2.Controls.Add(this.label23);
            this.splitContainer1.Panel2.Controls.Add(this.lblIN);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.lblFW);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.lblEX);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.lblDT);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.lblCD);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.lblCC);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 663);
            this.splitContainer1.SplitterDistance = 560;
            this.splitContainer1.TabIndex = 25;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._txtIn);
            this.splitContainer2.Panel1.Controls.Add(this._btnCoreference);
            this.splitContainer2.Panel1.Controls.Add(this._btnSplit);
            this.splitContainer2.Panel1.Controls.Add(this._btnSimilarity);
            this.splitContainer2.Panel1.Controls.Add(this._btnNameFind);
            this.splitContainer2.Panel1.Controls.Add(this._btnGender);
            this.splitContainer2.Panel1.Controls.Add(this._btnTokenize);
            this.splitContainer2.Panel1.Controls.Add(this._btnParse);
            this.splitContainer2.Panel1.Controls.Add(this._btnChunk);
            this.splitContainer2.Panel1.Controls.Add(this._btnPosTag);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._txtOut);
            this.splitContainer2.Size = new System.Drawing.Size(800, 560);
            this.splitContainer2.SplitterDistance = 172;
            this.splitContainer2.TabIndex = 26;
            // 
            // lblWRB
            // 
            this.lblWRB.AutoSize = true;
            this.lblWRB.Location = new System.Drawing.Point(600, 57);
            this.lblWRB.Name = "lblWRB";
            this.lblWRB.Size = new System.Drawing.Size(13, 13);
            this.lblWRB.TabIndex = 161;
            this.lblWRB.Text = "0";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(561, 57);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(36, 13);
            this.label93.TabIndex = 160;
            this.label93.Text = "WRB";
            // 
            // lblWPDOLLAR
            // 
            this.lblWPDOLLAR.AutoSize = true;
            this.lblWPDOLLAR.Location = new System.Drawing.Point(600, 42);
            this.lblWPDOLLAR.Name = "lblWPDOLLAR";
            this.lblWPDOLLAR.Size = new System.Drawing.Size(13, 13);
            this.lblWPDOLLAR.TabIndex = 159;
            this.lblWPDOLLAR.Text = "0";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label95.Location = new System.Drawing.Point(561, 42);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(34, 13);
            this.label95.TabIndex = 158;
            this.label95.Text = "WP$";
            // 
            // lblWDT
            // 
            this.lblWDT.AutoSize = true;
            this.lblWDT.Location = new System.Drawing.Point(600, 27);
            this.lblWDT.Name = "lblWDT";
            this.lblWDT.Size = new System.Drawing.Size(13, 13);
            this.lblWDT.TabIndex = 157;
            this.lblWDT.Text = "0";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.Location = new System.Drawing.Point(561, 27);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(36, 13);
            this.label97.TabIndex = 156;
            this.label97.Text = "WDT";
            // 
            // lblVBZ
            // 
            this.lblVBZ.AutoSize = true;
            this.lblVBZ.Location = new System.Drawing.Point(600, 13);
            this.lblVBZ.Name = "lblVBZ";
            this.lblVBZ.Size = new System.Drawing.Size(13, 13);
            this.lblVBZ.TabIndex = 155;
            this.lblVBZ.Text = "0";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(561, 13);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(31, 13);
            this.label99.TabIndex = 154;
            this.label99.Text = "VBZ";
            // 
            // lblVBP
            // 
            this.lblVBP.AutoSize = true;
            this.lblVBP.Location = new System.Drawing.Point(382, 58);
            this.lblVBP.Name = "lblVBP";
            this.lblVBP.Size = new System.Drawing.Size(13, 13);
            this.lblVBP.TabIndex = 153;
            this.lblVBP.Text = "0";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label101.Location = new System.Drawing.Point(343, 58);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(31, 13);
            this.label101.TabIndex = 152;
            this.label101.Text = "VBP";
            // 
            // lblVBN
            // 
            this.lblVBN.AutoSize = true;
            this.lblVBN.Location = new System.Drawing.Point(382, 43);
            this.lblVBN.Name = "lblVBN";
            this.lblVBN.Size = new System.Drawing.Size(13, 13);
            this.lblVBN.TabIndex = 151;
            this.lblVBN.Text = "0";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.Location = new System.Drawing.Point(343, 43);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(32, 13);
            this.label103.TabIndex = 150;
            this.label103.Text = "VBN";
            // 
            // lblVBG
            // 
            this.lblVBG.AutoSize = true;
            this.lblVBG.Location = new System.Drawing.Point(382, 28);
            this.lblVBG.Name = "lblVBG";
            this.lblVBG.Size = new System.Drawing.Size(13, 13);
            this.lblVBG.TabIndex = 149;
            this.lblVBG.Text = "0";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label105.Location = new System.Drawing.Point(343, 28);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(32, 13);
            this.label105.TabIndex = 148;
            this.label105.Text = "VBG";
            // 
            // lblVBD
            // 
            this.lblVBD.AutoSize = true;
            this.lblVBD.Location = new System.Drawing.Point(382, 14);
            this.lblVBD.Name = "lblVBD";
            this.lblVBD.Size = new System.Drawing.Size(13, 13);
            this.lblVBD.TabIndex = 147;
            this.lblVBD.Text = "0";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(343, 14);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(32, 13);
            this.label107.TabIndex = 146;
            this.label107.Text = "VBD";
            // 
            // lblSYM
            // 
            this.lblSYM.AutoSize = true;
            this.lblSYM.Location = new System.Drawing.Point(529, 27);
            this.lblSYM.Name = "lblSYM";
            this.lblSYM.Size = new System.Drawing.Size(13, 13);
            this.lblSYM.TabIndex = 145;
            this.lblSYM.Text = "0";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(490, 27);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(33, 13);
            this.label37.TabIndex = 144;
            this.label37.Text = "SYM";
            // 
            // lblRBS
            // 
            this.lblRBS.AutoSize = true;
            this.lblRBS.Location = new System.Drawing.Point(529, 13);
            this.lblRBS.Name = "lblRBS";
            this.lblRBS.Size = new System.Drawing.Size(13, 13);
            this.lblRBS.TabIndex = 143;
            this.lblRBS.Text = "0";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(490, 13);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(32, 13);
            this.label39.TabIndex = 142;
            this.label39.Text = "RBS";
            // 
            // lblRBR
            // 
            this.lblRBR.AutoSize = true;
            this.lblRBR.Location = new System.Drawing.Point(311, 58);
            this.lblRBR.Name = "lblRBR";
            this.lblRBR.Size = new System.Drawing.Size(13, 13);
            this.lblRBR.TabIndex = 141;
            this.lblRBR.Text = "0";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(272, 58);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(33, 13);
            this.label41.TabIndex = 140;
            this.label41.Text = "RBR";
            // 
            // lblPRPDOLLAR
            // 
            this.lblPRPDOLLAR.AutoSize = true;
            this.lblPRPDOLLAR.Location = new System.Drawing.Point(311, 43);
            this.lblPRPDOLLAR.Name = "lblPRPDOLLAR";
            this.lblPRPDOLLAR.Size = new System.Drawing.Size(13, 13);
            this.lblPRPDOLLAR.TabIndex = 139;
            this.lblPRPDOLLAR.Text = "0";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(272, 43);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(39, 13);
            this.label43.TabIndex = 138;
            this.label43.Text = "PRP$";
            // 
            // lblPRP
            // 
            this.lblPRP.AutoSize = true;
            this.lblPRP.Location = new System.Drawing.Point(311, 28);
            this.lblPRP.Name = "lblPRP";
            this.lblPRP.Size = new System.Drawing.Size(13, 13);
            this.lblPRP.TabIndex = 137;
            this.lblPRP.Text = "0";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(272, 28);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(32, 13);
            this.label45.TabIndex = 136;
            this.label45.Text = "PRP";
            // 
            // lblPOS
            // 
            this.lblPOS.AutoSize = true;
            this.lblPOS.Location = new System.Drawing.Point(311, 14);
            this.lblPOS.Name = "lblPOS";
            this.lblPOS.Size = new System.Drawing.Size(13, 13);
            this.lblPOS.TabIndex = 135;
            this.lblPOS.Text = "0";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(272, 14);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(32, 13);
            this.label47.TabIndex = 134;
            this.label47.Text = "POS";
            // 
            // lblPDT
            // 
            this.lblPDT.AutoSize = true;
            this.lblPDT.Location = new System.Drawing.Point(459, 27);
            this.lblPDT.Name = "lblPDT";
            this.lblPDT.Size = new System.Drawing.Size(13, 13);
            this.lblPDT.TabIndex = 133;
            this.lblPDT.Text = "0";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(417, 27);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(32, 13);
            this.label49.TabIndex = 132;
            this.label49.Text = "PDT";
            // 
            // lblNNPS
            // 
            this.lblNNPS.AutoSize = true;
            this.lblNNPS.Location = new System.Drawing.Point(459, 13);
            this.lblNNPS.Name = "lblNNPS";
            this.lblNNPS.Size = new System.Drawing.Size(13, 13);
            this.lblNNPS.TabIndex = 131;
            this.lblNNPS.Text = "0";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(417, 13);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(41, 13);
            this.label51.TabIndex = 130;
            this.label51.Text = "NNPS";
            // 
            // lblNNP
            // 
            this.lblNNP.AutoSize = true;
            this.lblNNP.Location = new System.Drawing.Point(241, 58);
            this.lblNNP.Name = "lblNNP";
            this.lblNNP.Size = new System.Drawing.Size(13, 13);
            this.lblNNP.TabIndex = 129;
            this.lblNNP.Text = "0";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(199, 58);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(33, 13);
            this.label53.TabIndex = 128;
            this.label53.Text = "NNP";
            // 
            // lblNNS
            // 
            this.lblNNS.AutoSize = true;
            this.lblNNS.Location = new System.Drawing.Point(241, 43);
            this.lblNNS.Name = "lblNNS";
            this.lblNNS.Size = new System.Drawing.Size(13, 13);
            this.lblNNS.TabIndex = 127;
            this.lblNNS.Text = "0";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(199, 43);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(33, 13);
            this.label55.TabIndex = 126;
            this.label55.Text = "NNS";
            // 
            // lblJJS
            // 
            this.lblJJS.AutoSize = true;
            this.lblJJS.Location = new System.Drawing.Point(241, 28);
            this.lblJJS.Name = "lblJJS";
            this.lblJJS.Size = new System.Drawing.Size(13, 13);
            this.lblJJS.TabIndex = 125;
            this.lblJJS.Text = "0";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(199, 28);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(27, 13);
            this.label57.TabIndex = 124;
            this.label57.Text = "JJS";
            // 
            // lblJJR
            // 
            this.lblJJR.AutoSize = true;
            this.lblJJR.Location = new System.Drawing.Point(241, 14);
            this.lblJJR.Name = "lblJJR";
            this.lblJJR.Size = new System.Drawing.Size(13, 13);
            this.lblJJR.TabIndex = 123;
            this.lblJJR.Text = "0";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(199, 14);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(28, 13);
            this.label59.TabIndex = 122;
            this.label59.Text = "JJR";
            // 
            // lblWP
            // 
            this.lblWP.AutoSize = true;
            this.lblWP.Location = new System.Drawing.Point(166, 57);
            this.lblWP.Name = "lblWP";
            this.lblWP.Size = new System.Drawing.Size(13, 13);
            this.lblWP.TabIndex = 121;
            this.lblWP.Text = "0";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(132, 57);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(27, 13);
            this.label29.TabIndex = 120;
            this.label29.Text = "WP";
            // 
            // lblVB
            // 
            this.lblVB.AutoSize = true;
            this.lblVB.Location = new System.Drawing.Point(166, 42);
            this.lblVB.Name = "lblVB";
            this.lblVB.Size = new System.Drawing.Size(13, 13);
            this.lblVB.TabIndex = 119;
            this.lblVB.Text = "0";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(132, 42);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(23, 13);
            this.label31.TabIndex = 118;
            this.label31.Text = "VB";
            // 
            // lblUH
            // 
            this.lblUH.AutoSize = true;
            this.lblUH.Location = new System.Drawing.Point(166, 27);
            this.lblUH.Name = "lblUH";
            this.lblUH.Size = new System.Drawing.Size(13, 13);
            this.lblUH.TabIndex = 117;
            this.lblUH.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(132, 27);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(25, 13);
            this.label33.TabIndex = 116;
            this.label33.Text = "UH";
            // 
            // lblTO
            // 
            this.lblTO.AutoSize = true;
            this.lblTO.Location = new System.Drawing.Point(166, 13);
            this.lblTO.Name = "lblTO";
            this.lblTO.Size = new System.Drawing.Size(13, 13);
            this.lblTO.TabIndex = 115;
            this.lblTO.Text = "0";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(132, 13);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(24, 13);
            this.label35.TabIndex = 114;
            this.label35.Text = "TO";
            // 
            // lblRP
            // 
            this.lblRP.AutoSize = true;
            this.lblRP.Location = new System.Drawing.Point(529, 57);
            this.lblRP.Name = "lblRP";
            this.lblRP.Size = new System.Drawing.Size(13, 13);
            this.lblRP.TabIndex = 113;
            this.lblRP.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(496, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 112;
            this.label13.Text = "RP";
            // 
            // lblRB
            // 
            this.lblRB.AutoSize = true;
            this.lblRB.Location = new System.Drawing.Point(529, 42);
            this.lblRB.Name = "lblRB";
            this.lblRB.Size = new System.Drawing.Size(13, 13);
            this.lblRB.TabIndex = 111;
            this.lblRB.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(496, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 13);
            this.label15.TabIndex = 110;
            this.label15.Text = "RB";
            // 
            // lblNN
            // 
            this.lblNN.AutoSize = true;
            this.lblNN.Location = new System.Drawing.Point(101, 57);
            this.lblNN.Name = "lblNN";
            this.lblNN.Size = new System.Drawing.Size(13, 13);
            this.lblNN.TabIndex = 109;
            this.lblNN.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(73, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 13);
            this.label17.TabIndex = 108;
            this.label17.Text = "NN";
            // 
            // lblMD
            // 
            this.lblMD.AutoSize = true;
            this.lblMD.Location = new System.Drawing.Point(101, 42);
            this.lblMD.Name = "lblMD";
            this.lblMD.Size = new System.Drawing.Size(13, 13);
            this.lblMD.TabIndex = 107;
            this.lblMD.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(73, 42);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(26, 13);
            this.label19.TabIndex = 106;
            this.label19.Text = "MD";
            // 
            // lblLS
            // 
            this.lblLS.AutoSize = true;
            this.lblLS.Location = new System.Drawing.Point(101, 27);
            this.lblLS.Name = "lblLS";
            this.lblLS.Size = new System.Drawing.Size(13, 13);
            this.lblLS.TabIndex = 105;
            this.lblLS.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(73, 27);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(22, 13);
            this.label21.TabIndex = 104;
            this.label21.Text = "LS";
            // 
            // lblJJ
            // 
            this.lblJJ.AutoSize = true;
            this.lblJJ.Location = new System.Drawing.Point(101, 13);
            this.lblJJ.Name = "lblJJ";
            this.lblJJ.Size = new System.Drawing.Size(13, 13);
            this.lblJJ.TabIndex = 103;
            this.lblJJ.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(73, 13);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(19, 13);
            this.label23.TabIndex = 102;
            this.label23.Text = "JJ";
            // 
            // lblIN
            // 
            this.lblIN.AutoSize = true;
            this.lblIN.Location = new System.Drawing.Point(459, 57);
            this.lblIN.Name = "lblIN";
            this.lblIN.Size = new System.Drawing.Size(13, 13);
            this.lblIN.TabIndex = 101;
            this.lblIN.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(417, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 100;
            this.label11.Text = "IN";
            // 
            // lblFW
            // 
            this.lblFW.AutoSize = true;
            this.lblFW.Location = new System.Drawing.Point(459, 42);
            this.lblFW.Name = "lblFW";
            this.lblFW.Size = new System.Drawing.Size(13, 13);
            this.lblFW.TabIndex = 99;
            this.lblFW.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(417, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 98;
            this.label9.Text = "FW";
            // 
            // lblEX
            // 
            this.lblEX.AutoSize = true;
            this.lblEX.Location = new System.Drawing.Point(40, 57);
            this.lblEX.Name = "lblEX";
            this.lblEX.Size = new System.Drawing.Size(13, 13);
            this.lblEX.TabIndex = 97;
            this.lblEX.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 96;
            this.label7.Text = "EX";
            // 
            // lblDT
            // 
            this.lblDT.AutoSize = true;
            this.lblDT.Location = new System.Drawing.Point(40, 42);
            this.lblDT.Name = "lblDT";
            this.lblDT.Size = new System.Drawing.Size(13, 13);
            this.lblDT.TabIndex = 95;
            this.lblDT.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "DT";
            // 
            // lblCD
            // 
            this.lblCD.AutoSize = true;
            this.lblCD.Location = new System.Drawing.Point(40, 27);
            this.lblCD.Name = "lblCD";
            this.lblCD.Size = new System.Drawing.Size(13, 13);
            this.lblCD.TabIndex = 93;
            this.lblCD.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "CD";
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.Location = new System.Drawing.Point(40, 13);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(13, 13);
            this.lblCC.TabIndex = 91;
            this.lblCC.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 90;
            this.label1.Text = "CC";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(731, 9);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(43, 23);
            this.btnFind.TabIndex = 162;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(650, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 163;
            this.label2.Text = "Find these:";
            // 
            // cboFind
            // 
            this.cboFind.FormattingEnabled = true;
            this.cboFind.Location = new System.Drawing.Point(653, 35);
            this.cboFind.Name = "cboFind";
            this.cboFind.Size = new System.Drawing.Size(121, 21);
            this.cboFind.TabIndex = 164;
            // 
            // _txtOut
            // 
            this._txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtOut.Location = new System.Drawing.Point(0, 0);
            this._txtOut.Name = "_txtOut";
            this._txtOut.Size = new System.Drawing.Size(800, 384);
            this._txtOut.TabIndex = 16;
            this._txtOut.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(800, 663);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "OpenNLP Tools Example";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}


	    internal void LoadPOSTags()
	    {
	        cboFind.Items.Add("CC");
	        cboFind.Items.Add("CD");
	        cboFind.Items.Add("DT");
	        cboFind.Items.Add("EX");
	        cboFind.Items.Add("FW");
	        cboFind.Items.Add("IN");
	        cboFind.Items.Add("JJ");
	        cboFind.Items.Add("JJR");
	        cboFind.Items.Add("JJS");
	        cboFind.Items.Add("LS");
	        cboFind.Items.Add("MD");
	        cboFind.Items.Add("NN");
	        cboFind.Items.Add("NNS");
	        cboFind.Items.Add("NNP");
	        cboFind.Items.Add("NNPS");
	        cboFind.Items.Add("PDT");
	        cboFind.Items.Add("POS");
	        cboFind.Items.Add("PRP");
	        cboFind.Items.Add("PRP$");
	        cboFind.Items.Add("RB");
	        cboFind.Items.Add("RBR");
	        cboFind.Items.Add("RBS");
	        cboFind.Items.Add("RP");
	        cboFind.Items.Add("SYM");
	        cboFind.Items.Add("TO");
	        cboFind.Items.Add("UH");
	        cboFind.Items.Add("VB");
	        cboFind.Items.Add("VBD");
	        cboFind.Items.Add("VBG");
	        cboFind.Items.Add("VBN");
	        cboFind.Items.Add("VBP");
	        cboFind.Items.Add("VBZ");
	        cboFind.Items.Add("WDT");
	        cboFind.Items.Add("WP");
	        cboFind.Items.Add("WP$");
	        cboFind.Items.Add("WRB");
	        cboFind.SelectedIndex = 0;
	    }
		// NLP methods -------------------------------------------

		private string[] SplitSentences(string paragraph)
		{
			if (_sentenceDetector == null)
			{
				_sentenceDetector = new EnglishMaximumEntropySentenceDetector(_modelPath + CommonFiles.EnglishSD_nbin);
			}

            Ensure.ArgumentNotNull(_sentenceDetector, nameof(_sentenceDetector));
			return _sentenceDetector.SentenceDetect(paragraph);
		}

		private string[] TokenizeSentence(string sentence)
		{
			if (_tokenizer == null)
			{
				_tokenizer = new EnglishRuleBasedTokenizer(false);
			}
		    Ensure.ArgumentNotNull(_tokenizer, nameof(_tokenizer));

            return _tokenizer.Tokenize(sentence);
		}

		private string[] PosTagTokens(string[] tokens)
		{
			if (_posTagger == null)
			{
				_posTagger = new EnglishMaximumEntropyPosTagger(_modelPath + CommonFiles.EnglishPOS_nbin, _modelPath + CommonDirectories.Parser_TagDict);
			}
		    Ensure.ArgumentNotNull(_posTagger, nameof(_posTagger));

            return _posTagger.Tag(tokens);
		}

		private string ChunkSentence(string[] tokens, string[] tags)
		{
			if (_chunker == null)
			{
				_chunker = new EnglishTreebankChunker(_modelPath + CommonFiles.EnglishChunk_nbin);
		    }
		    Ensure.ArgumentNotNull(_chunker, nameof(_chunker));

            return string.Join(CommonStrings.Space, _chunker.GetChunks(tokens, tags));
		}

		private Parse ParseSentence(string sentence)
		{
			if (_parser == null)
			{
				_parser = new EnglishTreebankParser(_modelPath, true, false);
			}
		    Ensure.ArgumentNotNull(_parser, nameof(_parser));

            return _parser.DoParse(sentence);
		}

		private string FindNames(string sentence)
		{
			if (_nameFinder == null)
			{
				_nameFinder = new EnglishNameFinder(_modelPath + CommonDirectories.NameFindDir);
			}
		    Ensure.ArgumentNotNull(_nameFinder, nameof(_nameFinder));

            var models = new[] {CommonModels.Date, CommonModels.Location, CommonModels.Money, CommonModels.Organization, CommonModels.Percentage, CommonModels.Person, CommonModels.Time};
			return _nameFinder.GetNames(models, sentence);
		}

        private string FindNames(Parse sentenceParse)
        {
            if (_nameFinder == null)
            {
                _nameFinder = new EnglishNameFinder(_modelPath + CommonDirectories.NameFindDir);
            }
            Ensure.ArgumentNotNull(_nameFinder, nameof(_nameFinder));

            var models = new[] { CommonModels.Date, CommonModels.Location, CommonModels.Money, CommonModels.Organization, CommonModels.Percentage, CommonModels.Person, CommonModels.Time };
            return _nameFinder.GetNames(models, sentenceParse);
        }

        private string IdentifyCoreferents(IEnumerable<string> sentences)
        {
            if (_coreferenceFinder == null)
            {
                _coreferenceFinder = new TreebankLinker(_modelPath + CommonLinkers.Coref);
            }
            Ensure.ArgumentNotNull(_coreferenceFinder, nameof(_coreferenceFinder));

            return _coreferenceFinder.GetCoreferenceParse(sentences.Select(ParseSentence).ToArray());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InitPOSMeasurements()
        {
            if (POSTagDict.Count > 0)
            {
                POSTagDict["CC"] = 0L;
                POSTagDict["CD"] = 0L;
                POSTagDict["DT"] = 0L;
                POSTagDict["EX"] = 0L;
                POSTagDict["FW"] = 0L;
                POSTagDict["IN"] = 0L;
                POSTagDict["JJ"] = 0L;
                POSTagDict["JJR"] = 0L;
                POSTagDict["JJS"] = 0L;
                POSTagDict["LS"] = 0L;
                POSTagDict["MD"] = 0L;
                POSTagDict["NN"] = 0L;
                POSTagDict["NNS"] = 0L;
                POSTagDict["NNP"] = 0L;
                POSTagDict["NNPS"] = 0L;
                POSTagDict["PDT"] = 0L;
                POSTagDict["POS"] = 0L;
                POSTagDict["PRP"] = 0L;
                POSTagDict["PRP$"] = 0L;
                POSTagDict["RB"] = 0L;
                POSTagDict["RBR"] = 0L;
                POSTagDict["RBS"] = 0L;
                POSTagDict["RP"] = 0L;
                POSTagDict["SYM"] = 0L;
                POSTagDict["TO"] = 0L;
                POSTagDict["UH"] = 0L;
                POSTagDict["VB"] = 0L;
                POSTagDict["VBD"] = 0L;
                POSTagDict["VBG"] = 0L;
                POSTagDict["VBN"] = 0L;
                POSTagDict["VBP"] = 0L;
                POSTagDict["VBZ"] = 0L;
                POSTagDict["WDT"] = 0L;
                POSTagDict["WP"] = 0L;
                POSTagDict["WP$"] = 0L;
                POSTagDict["UH"] = 0L;
                POSTagDict["WRB"] = 0L;
                return;
            }

            POSTagDict.Clear();
            POSTagDict.Add("CC", 0L);
            POSTagDict.Add("CD", 0L);
            POSTagDict.Add("DT", 0L);
            POSTagDict.Add("EX", 0L);
            POSTagDict.Add("FW", 0L);
            POSTagDict.Add("IN", 0L);
            POSTagDict.Add("JJ", 0L);
            POSTagDict.Add("JJR", 0L);
            POSTagDict.Add("JJS", 0L);
            POSTagDict.Add("LS", 0L);
            POSTagDict.Add("MD", 0L);
            POSTagDict.Add("NN", 0L);
            POSTagDict.Add("NNS", 0L);
            POSTagDict.Add("NNP", 0L);
            POSTagDict.Add("NNPS", 0L);
            POSTagDict.Add("PDT", 0L);
            POSTagDict.Add("POS", 0L);
            POSTagDict.Add("PRP", 0L);
            POSTagDict.Add("PRP$", 0L);
            POSTagDict.Add("RB", 0L);
            POSTagDict.Add("RBR", 0L);
            POSTagDict.Add("RBS", 0L);
            POSTagDict.Add("RP", 0L);
            POSTagDict.Add("SYM", 0L);
            POSTagDict.Add("TO", 0L);
            POSTagDict.Add("UH", 0L);
            POSTagDict.Add("VB", 0L);
            POSTagDict.Add("VBD", 0L);
            POSTagDict.Add("VBG", 0L);
            POSTagDict.Add("VBN", 0L);
            POSTagDict.Add("VBP", 0L);
            POSTagDict.Add("VBZ", 0L);
            POSTagDict.Add("WDT", 0L);
            POSTagDict.Add("WP", 0L);
            POSTagDict.Add("WP$", 0L);
            POSTagDict.Add("WRB", 0L);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdatePOSMeasurements()
        {
            lblCC.Text = POSTagDict["CC"].ToString("N0");
            lblCD.Text = POSTagDict["CD"].ToString("N0");
            lblDT.Text = POSTagDict["DT"].ToString("N0");
            lblEX.Text = POSTagDict["EX"].ToString("N0");
            lblFW.Text = POSTagDict["FW"].ToString("N0");
            lblIN.Text = POSTagDict["IN"].ToString("N0");
            lblJJ.Text = POSTagDict["JJ"].ToString("N0");
            lblJJR.Text = POSTagDict["JJR"].ToString("N0");
            lblJJS.Text = POSTagDict["JJS"].ToString("N0");
            lblLS.Text = POSTagDict["LS"].ToString("N0");
            lblMD.Text = POSTagDict["MD"].ToString("N0");
            lblNN.Text = POSTagDict["NN"].ToString("N0");
            lblNNS.Text = POSTagDict["NNS"].ToString("N0");
            lblNNP.Text = POSTagDict["NNP"].ToString("N0");
            lblNNPS.Text = POSTagDict["NNPS"].ToString("N0");
            lblPDT.Text = POSTagDict["PDT"].ToString("N0");
            lblPOS.Text = POSTagDict["POS"].ToString("N0");
            lblPRP.Text = POSTagDict["PRP"].ToString("N0");
            lblPRPDOLLAR.Text = POSTagDict["PRP$"].ToString("N0");
            lblRB.Text = POSTagDict["RB"].ToString("N0");
            lblRBR.Text = POSTagDict["RBR"].ToString("N0");
            lblRBS.Text = POSTagDict["RBS"].ToString("N0");
            lblRP.Text = POSTagDict["RP"].ToString("N0");
            lblSYM.Text = POSTagDict["SYM"].ToString("N0");
            lblTO.Text = POSTagDict["TO"].ToString("N0");
            lblUH.Text = POSTagDict["UH"].ToString("N0");
            lblVB.Text = POSTagDict["VB"].ToString("N0");
            lblVBD.Text = POSTagDict["VBD"].ToString("N0");
            lblVBG.Text = POSTagDict["VBG"].ToString("N0");
            lblVBN.Text = POSTagDict["VBN"].ToString("N0");
            lblVBP.Text = POSTagDict["VBP"].ToString("N0");
            lblVBZ.Text = POSTagDict["VBZ"].ToString("N0");
            lblWDT.Text = POSTagDict["WDT"].ToString("N0");
            lblWP.Text = POSTagDict["WP"].ToString("N0");
            lblWPDOLLAR.Text = POSTagDict["WP$"].ToString("N0");
            lblWRB.Text = POSTagDict["WRB"].ToString("N0");
        }

        private void _btnSplit_Click(object sender, EventArgs e)
        {
            var sentences = SplitSentences(_txtIn.Text);
            _txtOut.Text = string.Join($"{Environment.NewLine}{Environment.NewLine}", sentences);
        }

        private void _btnTokenize_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            var sentences = SplitSentences(_txtIn.Text);

            foreach (var sentence in sentences)
            {
                var tokens = TokenizeSentence(sentence);
                output.Append(string.Join(CommonStrings.PipeTokenWithSpaces, tokens)).Append($"{Environment.NewLine}{Environment.NewLine}");
            }

            _txtOut.Text = output.ToString();
        }

        private void _btnPosTag_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();
            InitPOSMeasurements();

            var sentences = SplitSentences(_txtIn.Text);

            foreach (var sentence in sentences)
            {
                var tokens = TokenizeSentence(sentence);
                var tags = PosTagTokens(tokens);

                for (var currentTag = 0; currentTag < tags.Length; currentTag++)
                {
                    if (!POSTagDict.ContainsKey(tags[currentTag]))
                        POSTagDict.Add(tags[currentTag], 1);
                    else
                    {
                        POSTagDict[tags[currentTag]] += 1;
                    }
                    output.Append(tokens[currentTag]).Append(CommonStrings.ForwardSlash).Append(tags[currentTag]).Append(CommonStrings.Space);
                }

                output.Append($"{Environment.NewLine}{Environment.NewLine}");
            }

            _txtOut.Text = output.ToString();
            UpdatePOSMeasurements();
        }

        private void _btnChunk_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            var sentences = SplitSentences(_txtIn.Text);

            foreach (var sentence in sentences)
            {
                var tokens = TokenizeSentence(sentence);
                var tags = PosTagTokens(tokens);

                output.Append(ChunkSentence(tokens, tags)).Append($"{Environment.NewLine}{Environment.NewLine}");
            }

            _txtOut.Text = output.ToString();
        }

        private void _btnParse_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            var sentences = SplitSentences(_txtIn.Text);

            foreach (var sentence in sentences)
            {
                var parse = ParseSentence(sentence);
                output.Append(parse.Show()).Append($"{Environment.NewLine}{Environment.NewLine}");
            }

            _txtOut.Text = output.ToString();

        }

        private void _btnNameFind_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            var sentences = SplitSentences(_txtIn.Text);

            foreach (var sentence in sentences)
            {
                output.Append(FindNames(sentence)).Append($"{Environment.NewLine}");
            }

            _txtOut.Text = output.ToString();
        }

        private void _btnGender_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            var sentences = SplitSentences(_txtIn.Text);
            foreach (var sentence in sentences)
            {
                var tokens = TokenizeSentence(sentence);
                var tags = PosTagTokens(tokens);

                var posTaggedSentence = string.Empty;
                for (var currentTag = 0; currentTag < tags.Length; currentTag++)
                {
                    posTaggedSentence += tokens[currentTag] + CommonStrings.ForwardSlash + tags[currentTag] + CommonStrings.Space;
                }

                output.Append(posTaggedSentence);
                output.Append($"{Environment.NewLine}");
                output.Append(GenderModel.GenderMain(_modelPath + CommonDirectories.CorefGenDir, posTaggedSentence));
                output.Append($"{Environment.NewLine}{Environment.NewLine}");
            }

            _txtOut.Text = output.ToString();
        }

        private void _btnSimilarity_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();
            string[] sentences = SplitSentences(_txtIn.Text);

            foreach (string sentence in sentences)
            {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);
                string posTaggedSentence = string.Empty;

                for (int currentTag = 0; currentTag < tags.Length; currentTag++)
                {
                    posTaggedSentence += tokens[currentTag] + @"/" + tags[currentTag] + CommonStrings.Space;
                }
                
                output.Append(posTaggedSentence);
                output.Append($"{Environment.NewLine}");
                output.Append(SimilarityModel.SimilarityMain(_modelPath + CommonDirectories.CorefSimDir, posTaggedSentence));
                output.Append($"{Environment.NewLine}{Environment.NewLine}");
            }

            _txtOut.Text = output.ToString();
        }

        private void _btnCoreference_Click(object sender, EventArgs e)
        {
            var sentences = SplitSentences(_txtIn.Text);
            Ensure.ArgumentNotNull(sentences, nameof(sentences));
            _txtOut.Text = IdentifyCoreferents(sentences);
        }

	    public void AppendText(RichTextBox box, string text, Color color)
	    {
	        box.SelectionStart = box.TextLength;
	        box.SelectionLength = text.Length;
	        box.SelectionColor = color;
	        box.AppendText(text);
	        box.SelectionColor = box.ForeColor;
	    }

	    public void HighlightText(RichTextBox myRtb, string word, Color color)
	    {
	        if (word == string.Empty)
	            return;

	        int sStart = myRtb.SelectionStart, startIndex = 0, index;

	        while ((index = myRtb.Text.IndexOf(word, startIndex, StringComparison.Ordinal)) != -1)
	        {
	            myRtb.Select(index, word.Length);
	            myRtb.SelectionColor = color;
	            myRtb.SelectionFont = new Font("Georgia", 16);
                startIndex = index + word.Length;
	        }

	        myRtb.SelectionStart = sStart;
	        myRtb.SelectionLength = 0;
	        myRtb.SelectionColor = Color.Black;
	    }

	    public void BoldText(RichTextBox myRtb, string word)
	    {
	        if (word == string.Empty)
	            return;

	        int sStart = myRtb.SelectionStart, startIndex = 0, index;

	        while ((index = myRtb.Text.IndexOf(word, startIndex, StringComparison.Ordinal)) != -1)
	        {
	            myRtb.Select(index, word.Length);
	            myRtb.SelectionColor = color;
	            myRtb.SelectionFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
	            startIndex = index + word.Length;
            }

	        myRtb.SelectionStart = sStart;
	        myRtb.SelectionLength = 0;
	        myRtb.SelectionColor = Color.Black;
	    }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _btnPosTag_Click(sender, e);
            HighlightText(_txtOut, cboFind.SelectedItem as string + CommonStrings.Space, Color.Red);
        }
    }
}
