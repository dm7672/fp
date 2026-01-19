using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloud.Domain.Model;

namespace TagsCloud.Interfaces
{
    public interface IPartOfSpeechFilter
    {
        bool IsAllowed(PartOfSpeech pos);
    }
}
