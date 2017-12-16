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

//This file is based on the AbstractParse.java source file found in the
//original java implementation of OpenNLP.

using System;
using System.Collections.Generic;

namespace OpenNLP.Tools.Coreference.Mention
{
	
	/// <summary>
    /// Provides default implemenation of many of the methods in the IParse interface.
    /// </summary>
	public abstract class AbstractParse : IParse
	{
		public virtual bool IsCoordinatedNounPhrase
		{
			get
			{
				var parts = SyntacticChildren;
				if (parts.Count >= 2)
				{
					for (var currentPart = 1; currentPart < parts.Count; currentPart++)
					{
						var child = parts[currentPart];
						var childType = child.SyntacticType;
                        if (childType != null && childType == PartsOfSpeechStrings.CoordinatingConjunction && !(child.ToString() == "&"))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public virtual List<IParse> NounPhrases
		{
			get
			{
                var parts = SyntacticChildren;
                var nounPhrases = new List<IParse>();
				while (parts.Count > 0)
				{
                    var newParts = new List<IParse>();
					for (var currentPart = 0; currentPart < parts.Count; currentPart++)
					{
						var currentPartParse = parts[currentPart];
						if (currentPartParse.IsNounPhrase)
						{
							nounPhrases.Add(currentPartParse);
						}
						if (!currentPartParse.IsToken)
						{
                            newParts.AddRange(currentPartParse.SyntacticChildren);
						}
					}
					parts = newParts;
				}
				return nounPhrases;
			}	
		}

        public abstract List<IParse> NamedEntities { get;}
		public abstract Util.Span Span{get;}
        public abstract List<IParse> Tokens { get;}
        public abstract List<IParse> Children { get;}
		public abstract IParse NextToken{get;}
		public abstract IParse PreviousToken{get;}
		public abstract List<IParse> SyntacticChildren{get;}
		public abstract string SyntacticType{get;}
		public abstract bool ParentNac{get;}
		public abstract bool IsToken{get;}
		public abstract string EntityType{get;}
		public abstract bool IsNamedEntity{get;}
		public abstract bool IsNounPhrase{get;}
		public abstract bool IsSentence{get;}
		public abstract int EntityId{get;}
		public abstract IParse Parent{get;}
		public abstract int SentenceNumber{get;}
		public abstract int CompareTo(object obj);
	}
}