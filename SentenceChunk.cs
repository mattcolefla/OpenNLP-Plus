using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNLP.Tools.Chunker
{
    public class SentenceChunk
    {
        // Properties ----------------------

        public int IndexInSentence { get; }
        public string Tag { get; }
        public List<TaggedWord> TaggedWords { get; }


        // Constructors --------------------

        public SentenceChunk(int index)
        {
            IndexInSentence = index;
            TaggedWords = new List<TaggedWord>();
        }

        public SentenceChunk(string tag, int index):this(index)
        {
            Tag = tag;
        }


        // Methods ------------------------

        public override string ToString()
        {
            return $"[{(!string.IsNullOrWhiteSpace(Tag) ? Tag + CommonStrings.Space : string.Empty)}{string.Join(CommonStrings.Space, TaggedWords)}]";
        }
    }
}
