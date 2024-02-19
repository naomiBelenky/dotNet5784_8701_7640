using System.Collections;
using System.Collections.Generic;

namespace PL;

internal class LevelsCollection : IEnumerable
{
    static readonly IEnumerable<BO.Level> s_enums =
        (Enum.GetValues(typeof(BO.Level)) as IEnumerable<BO.Level>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

internal class StatusCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enums =
        (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
