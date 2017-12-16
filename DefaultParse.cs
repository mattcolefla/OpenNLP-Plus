//Copyright (C) 2006 Richard J. Northedge
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

//This file is based on the DefaultParse.java source file found in the
//original java implementation of OpenNLP.  That source file contains the following header:

//Copyright (C) 2003 Thomas Morton
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this program; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using OpenNLP.Tools.Trees;
using NameFinder = OpenNLP.Tools.NameFind.EnglishNameFinder;
using Parse = OpenNLP.Tools.Parser.Parse;
using ParserME = OpenNLP.Tools.Parser.MaximumEntropyParser;
using Span = OpenNLP.Tools.Util.Span;
namespace OpenNLP.Tools.Coreference.Mention
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This class is a wrapper for {@link OpenNLP.Tools.Parser.Parse} mapping it to the API
    /// specified in
    /// {@link OpenNLP.Tools.Coreference.Mention.Parse}.
    /// This allows coreference to be done on the output of the parser.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class DefaultParse : AbstractParse
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns the index of the sentence which contains this parse. </summary>
        ///
        /// <value> The sentence number. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override int SentenceNumber => mSentenceNumber;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a list of all the named entities contained by this parse. </summary>
        ///
        /// <value> The named entities. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override List<IParse> NamedEntities
        {
            get
            {
                var names = new List<Parse>();
                var kids = new List<Parse>(mParse.GetChildren());

                while (kids.Count > 0)
                {
                    var currentParse = kids[0];
                    kids.RemoveAt(0);

                    if (mEntitySet.Contains(currentParse.Type))
                    {
                        names.Add(currentParse);
                    }
                    else
                    {
                        kids.AddRange(currentParse.GetChildren());
                    }
                }
                return CreateParses(names.ToArray());
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a list of the children to this object. </summary>
        ///
        /// <value> The children. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override List<IParse> Children => CreateParses(mParse.GetChildren());

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns a list of the children to this object which are constituents or tokens. This allows
        /// implementations which contain addition nodes for things such as semantic categories to hide
        /// those nodes from the components which only care about syntactic nodes.
        /// </summary>
        ///
        /// <value> The syntactic children. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override List<IParse> SyntacticChildren
        {
            get
            {
                var kids = new List<Parse>(mParse.GetChildren());
                for (var childIndex = 0; childIndex < kids.Count; childIndex++)
                {
                    var currentKid = kids[childIndex];
                    if (mEntitySet.Contains(currentKid.Type))
                    {
                        //remove currentKid
                        kids.RemoveAt(childIndex);

                        //and replace it with its children
                        kids.InsertRange(childIndex, currentKid.GetChildren());

                        //set childIndex back by one so we process the parses we just added
                        childIndex--;
                    }
                }
                return CreateParses(kids.ToArray());
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a list of the tokens contained by this object. </summary>
        ///
        /// <value> The tokens. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override List<IParse> Tokens
        {
            get
            {
                var tokens = new List<Parse>();
                var kids = new List<Parse>(mParse.GetChildren());
                while (kids.Count > 0)
                {
                    var currentParse = kids[0];
                    kids.RemoveAt(0);

                    if (currentParse.IsPosTag)
                    {
                        tokens.Add(currentParse);
                    }
                    else
                    {
                        kids.AddRange(currentParse.GetChildren());
                    }
                }
                return CreateParses(tokens.ToArray());
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the syntactic type of this node. Typically this is the part-of-speech or constituent
        /// labeling.
        /// </summary>
        ///
        /// <value> The type of the syntactic. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override string SyntacticType => mEntitySet.Contains(mParse.Type) ? null : mParse.Type;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns the named-entity type of this node. </summary>
        ///
        /// <value> The type of the entity. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override string EntityType => mEntitySet.Contains(mParse.Type) ? mParse.Type : null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determines whether this has an ancestor of type NAC. </summary>
        ///
        /// <value> True if parent nac, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool ParentNac
        {
            get
            {
                var parent = mParse.Parent;
                while (parent != null)
                {
                    if (parent.Type == AbstractCollinsHeadFinder.NAC)
                    {
                        return true;
                    }
                    parent = parent.Parent;
                }
                return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns the parent parse of this parse node. </summary>
        ///
        /// <value> The parent. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override IParse Parent
        {
            get
            {
                var parent = mParse.Parent;
                return parent == null ? null : new DefaultParse(parent, mSentenceNumber);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Specifies whether this parse is a named-entity. </summary>
        ///
        /// <value>
        /// True if this OpenNLP.Tools.Coreference.Mention.DefaultParse is named entity, false if not.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsNamedEntity => mEntitySet.Contains(mParse.Type);

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Specifies whether this parse is a noun phrase. </summary>
        ///
        /// <value>
        /// True if this OpenNLP.Tools.Coreference.Mention.DefaultParse is noun phrase, false if not.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsNounPhrase => mParse.Type == CoordinationTransformer.Noun;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Specifies whether this parse is a sentence. </summary>
        ///
        /// <value>
        /// True if this OpenNLP.Tools.Coreference.Mention.DefaultParse is sentence, false if not.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsSentence => mParse.Type == CommonStrings.TopNode;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Specifies whether this parse is a token. </summary>
        ///
        /// <value>
        /// True if this OpenNLP.Tools.Coreference.Mention.DefaultParse is token, false if not.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool IsToken => mParse.IsPosTag;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns an entity id associated with this parse and coreferent parses.  This is only used for
        /// training on already annotated coreference annotation.
        /// </summary>
        ///
        /// <value> The identifier of the entity. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override int EntityId { get; } = -1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns the character offsets of this parse node. </summary>
        ///
        /// <value> The span. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override Span Span => mParse.Span;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the first token which is not a child of this parse.  If the first token of a sentence
        /// is a child of this parse then null is returned.
        /// </summary>
        ///
        /// <value> The previous token. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override IParse PreviousToken
        {
            get
            {
                var parent = mParse.Parent;
                var node = mParse;
                var index = -1;
                //find parent with previous children
                while (parent != null && index < 0)
                {
                    index = parent.IndexOf(node) - 1;
                    if (index < 0)
                    {
                        node = parent;
                        parent = parent.Parent;
                    }
                }
                //find right-most child which is a token
                if (index < 0)
                {
                    return null;
                }

                var currentParse = parent?.GetChildren()[index];
                while (!currentParse.IsPosTag)
                {
                    var kids = currentParse.GetChildren();
                    currentParse = kids[kids.Length - 1];
                }
                return new DefaultParse(currentParse, mSentenceNumber);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the next token which is not a child of this parse.  If the last token of a sentence
        /// is a child of this parse then null is returned.
        /// </summary>
        ///
        /// <value> The next token. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override IParse NextToken
        {
            get
            {
                var parent = mParse.Parent;
                var node = mParse;
                var index = -1;
                //find parent with subsequent children
                while (parent != null)
                {
                    index = parent.IndexOf(node) + 1;
                    if (index == parent.ChildCount)
                    {
                        node = parent;
                        parent = parent.Parent;
                    }
                    else
                    {
                        break;
                    }
                }
                //find left-most child which is a token
                if (parent == null)
                {
                    return null;
                }
                var currentParse = parent.GetChildren()[index];
                while (!currentParse.IsPosTag)
                {
                    currentParse = currentParse.GetChildren()[0];
                }
                return new DefaultParse(currentParse, mSentenceNumber);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the parse. </summary>
        ///
        /// <value> The parse. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public virtual Parse Parse => mParse;

        /// <summary>   The parse. </summary>
        private Parse mParse;

        /// <summary>   The sentence number. </summary>
        private int mSentenceNumber;

        /// <summary>   Set the entity belongs to. </summary>
        private static Util.Set<string> mEntitySet = new Util.HashSet<string>(new List<string>(NameFinder.NameTypes));

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="parse">            The parse. </param>
        /// <param name="sentenceNumber">   The sentence number. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public DefaultParse(Parse parse, int sentenceNumber)
        {
            mParse = parse;
            mSentenceNumber = sentenceNumber;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates the parses. </summary>
        ///
        /// <param name="parses">   The parses. </param>
        ///
        /// <returns>   The new parses. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private List<IParse> CreateParses(Parse[] parses)
        {
            var newParses = new List<IParse>(parses.Length);
            for (int parseIndex = 0, parseCount = parses.Length; parseIndex < parseCount; parseIndex++)
            {
                newParses.Add(new DefaultParse(parses[parseIndex], mSentenceNumber));
            }
            return newParses;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Compares this object object to another to determine their relative ordering.
        /// </summary>
        ///
        /// <param name="obj">  Object to compare to this. </param>
        ///
        /// <returns>
        /// Negative if this OpenNLP.Tools.Coreference.Mention.DefaultParse is less than the other, 0 if
        /// they are equal, or positive if this is greater.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override int CompareTo(object obj)
        {
            if (obj == this)
            {
                return 0;
            }
            var p = (DefaultParse) obj;
            if (mSentenceNumber < p.mSentenceNumber)
            {
                return -1;
            }
            return mSentenceNumber > p.mSentenceNumber ? 1 : mParse.Span.CompareTo(p.Span);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a string that represents the current object. </summary>
        ///
        /// <returns>   A string that represents the current object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override string ToString()
        {
            return mParse.ToString();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determines whether the specified object is equal to the current object. </summary>
        ///
        /// <param name="obj">  The object to compare with the current object. </param>
        ///
        /// <returns>
        /// <see langword="true" /> if the specified object  is equal to the current object; otherwise,
        /// <see langword="false" />.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool Equals(object obj)
        {
            return (mParse == ((DefaultParse) obj)?.mParse);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Serves as the default hash function. </summary>
        ///
        /// <returns>   A hash code for the current object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecuritySafeCritical]
        public override int GetHashCode()
        {
            if (mParse != null)
                return (mParse.GetHashCode());

            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + SentenceNumber.GetHashCode();
                hash = hash * 23 + "NP".GetHashCode();
                hash = hash * 23 + "TP".GetHashCode();
                return hash;
            }
        }
    }
}