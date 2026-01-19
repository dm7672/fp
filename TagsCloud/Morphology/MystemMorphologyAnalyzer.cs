using System;
using System.Collections.Generic;
using System.Linq;
using MystemSharp; 
using TagsCloud.Domain.Model;
using TagsCloud.Interfaces;

namespace TagsCloud.Morphology
{
    public sealed class MystemSharpMorphology : IMorphologyAnalyzer
    {
        public MystemSharpMorphology()
        {

        }

        public Word Analyze(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return new Word(token ?? string.Empty, token ?? string.Empty, PartOfSpeech.Other); 

            var analyses = new Analyses(token);
            if (analyses == null || analyses.Count == 0)
                return new Word(token, token.ToLowerInvariant(), PartOfSpeech.Other);

            var lemma = analyses[0];
            var lemmaText = string.IsNullOrEmpty(lemma.Text) ? token : lemma.Text;
            var pos = DetectPartOfSpeechFromLemma(lemma);

            return new Word(token, lemmaText.ToLowerInvariant(), pos);
        }

        private static PartOfSpeech DetectPartOfSpeechFromLemma(Lemma lemma)
        {
            var stemGram = lemma.StemGram;
            if (stemGram != null && stemGram.Count > 0)
            {
                var posGram = stemGram.FirstOrDefault(g => g >= Grammar.First && g <= Grammar.LastPartOfSpeech);
                return MapGrammarToPartOfSpeech(posGram);
            }
            return PartOfSpeech.Other;
        }

        private static PartOfSpeech MapGrammarToPartOfSpeech(Grammar grammar)
        {
            return grammar switch
            {
                Grammar.Substantive => PartOfSpeech.Noun,          
                Grammar.Verb => PartOfSpeech.Verb,                 
                Grammar.Adjective => PartOfSpeech.Adjective,       
                Grammar.Adverb => PartOfSpeech.Adverb,             
                Grammar.SubstPronoun => PartOfSpeech.Pronoun,      
                Grammar.AdjPronoun or Grammar.AdvPronoun => PartOfSpeech.Pronoun,
                Grammar.Preposition => PartOfSpeech.Preposition,   
                Grammar.Conjunction => PartOfSpeech.Conjunction,   
                Grammar.Particle => PartOfSpeech.Particle,        
                _ => PartOfSpeech.Other
            };
        }
    }
}
