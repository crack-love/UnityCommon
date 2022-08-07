#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityCommon;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 2021-04-05 월 오후 12:40:41, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace UnityCommon
{
    // TODO : CHANGE IENUMERABLE TO STRUCT
    public static class Query
    {
        #region Where
        public static IEnumerable<TD> Cast<TS, TD>(IEnumerable<TS> src)
        {
            return EnumeratorFactory.GetCastEnumerable<TS, TD>(src);
        }

        public static IEnumerable<TD> Cast<TS, TD>(IList<TS> src)
        {
            var listEnumer = EnumeratorFactory.GetListEnumerable(src);

            return EnumeratorFactory.GetCastEnumerable<TS, TD>(listEnumer);
        }

        public static IEnumerable<T> Where<T>(IEnumerable<T> src, Func<T, bool> Predicter)
        {
            return EnumeratorFactory.GetWhereEnumerable<T>(src, Predicter);
        }

        public static IEnumerable<T> Where<T>(IList<T> src, Func<T, bool> Predicter)
        {
            var listEnumer = EnumeratorFactory.GetListEnumerable(src);

            return EnumeratorFactory.GetWhereEnumerable<T>(listEnumer, Predicter);
        }
        #endregion

        #region Select
        public static IEnumerable<TD> Select<TS, TD>(IEnumerable<TS> src, Func<TS, TD> selecter)
        {
            return EnumeratorFactory.GetSelectEnumerable(src, selecter);
        }

        public static IEnumerable<TD> Select<TS, TD>(IList<TS> src, Func<TS, TD> selecter)
        {
            var listEnumer = EnumeratorFactory.GetListEnumerable(src);

            return EnumeratorFactory.GetSelectEnumerable(listEnumer, selecter);
        }
        #endregion

        #region DISTINCT
        public static IEnumerable<T> Distinct<T>(IEnumerable<T> src)
        {
            return EnumeratorFactory.GetDistinctEnumerable(src);
        }

        public static IEnumerable<T> Distinct<T>(IList<T> src)
        {
            var listEnumer = EnumeratorFactory.GetListEnumerable(src);

            return EnumeratorFactory.GetDistinctEnumerable(listEnumer);
        }

        public static IEnumerable<T> Distinct<T>(IEnumerable<T> src, Func<T, T, bool> distinction)
        {
            return EnumeratorFactory.GetDistinctEnumerable(src, distinction);
        }

        public static IEnumerable<T> Distinct<T>(IList<T> src, Func<T, T, bool> distinction)
        {
            var listEnumer = EnumeratorFactory.GetListEnumerable(src);

            return EnumeratorFactory.GetDistinctEnumerable(listEnumer, distinction);
        }
        #endregion

        /// <summary>
        /// TODO : enumerable/enumerator pool, enumerator=pooling inside enumerable
        /// </summary>
        static class EnumeratorFactory
        {
            public static ListEnumerable<T> GetListEnumerable<T>(IList<T> list)
            {
                return new ListEnumerable<T>()
                {
                    List = list,
                };
            }

            public static IEnumerable<T> GetWhereEnumerable<T>(IEnumerable<T> src, Func<T, bool> Whereer)
            {
                return new WhereEnumerable<T>()
                {
                    Source = src,
                    Predicter = Whereer,
                };
            }

            public static IEnumerable<TD> GetCastEnumerable<TS, TD>(IEnumerable<TS> src)
            {
                return new CastEnumerable<TS, TD>()
                {
                    Source = src,
                };
            }

            public static IEnumerable<TD> GetSelectEnumerable<TS, TD>(IEnumerable<TS> src, Func<TS, TD> updater)
            {
                return new SelectEnumerable<TS, TD>()
                {
                    Source = src,
                    Selecter = updater,
                };
            }

            public static IEnumerable<T> GetDistinctEnumerable<T>(IEnumerable<T> src)
            {
                return new DistinctEnumerable<T>()
                {
                    Source = src,
                    Distinction = FuncDefaultDictinction,
                };
            }

            public static IEnumerable<T> GetDistinctEnumerable<T>(IEnumerable<T> src, Func<T, T, bool> distinction)
            {
                return new DistinctEnumerable<T>()
                {
                    Source = src,
                    Distinction = distinction,
                };
            }

            static bool FuncAlwaysReturnTrue<T>(T arg)
            {
                return true;
            }
            
            static bool FuncDefaultDictinction<T>(T left, T right)
            {
                return left.Equals(right);
            }
        }

        abstract class DecorateEnumerable<T> : DecorateEnumerable<T, T>
        {

        }

        abstract class DecorateEnumerable<TS, TD> : IEnumerable<TD>
        {
            protected IEnumerable<TS> m_source;

            public IEnumerable<TS> Source
            {
                set
                {
                    Check.Null(value);

                    m_source = value;
                }
            }

            public IEnumerator<TD> GetEnumerator()
            {
                var src = m_source.GetEnumerator();

                var res = GetDecorateEnumerator();

                res.Source = src;

                res.Reset();

                return res;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            protected abstract DecorateEnumerator GetDecorateEnumerator();

            protected abstract class DecorateEnumerator : IEnumerator<TD>
            {
                public IEnumerator<TS> Source;
                public TD Current;

                TD IEnumerator<TD>.Current => Current;

                object IEnumerator.Current => Current;

                public abstract bool MoveNext();

                public virtual void Dispose()
                {
                    if (Source is IDisposable dspSrc)
                    {
                        dspSrc.Dispose();
                    }

                    if (Current is IDisposable dspCurr)
                    {
                        dspCurr.Dispose();
                    }

                    Source = null;
                    Current = default;
                }

                public virtual void Reset()
                {
                    Source.Reset();
                }
            }
        }

        class WhereEnumerable<T> : DecorateEnumerable<T>
        {
            Func<T, bool> m_predicter;

            public Func<T, bool> Predicter
            {
                set
                {
                    Check.Null(value);

                    m_predicter = value;
                }
            }

            protected override DecorateEnumerator GetDecorateEnumerator()
            {
                return new WhereEnemerator()
                {
                    Predicter = m_predicter,
                };
            }

            class WhereEnemerator : DecorateEnumerator
            {
                public Func<T, bool> Predicter;

                public override void Dispose()
                {
                    base.Dispose();

                    Predicter = null;
                }

                public override bool MoveNext()
                {
                    while (Source.MoveNext())
                    {
                        var srcCurr = Source.Current;

                        if (Predicter(srcCurr))
                        {
                            Current = srcCurr;

                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        class CastEnumerable<TS, TD> : DecorateEnumerable<TS, TD>
        {
            protected override DecorateEnumerator GetDecorateEnumerator()
            {
                return new CastEnumerator();
            }

            class CastEnumerator : DecorateEnumerator
            {
                public override bool MoveNext()
                {
                    while (Source.MoveNext())
                    {
                        if (Source.Current is TD Convert)
                        {
                            Current = Convert;

                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        class SelectEnumerable<TS, TD> : DecorateEnumerable<TS, TD>
        {
            Func<TS, TD> m_selecter;

            public Func<TS, TD> Selecter
            {
                set
                {
                    Check.Null(value);

                    m_selecter = value;
                }
            }

            protected override DecorateEnumerator GetDecorateEnumerator()
            {
                return new SelectEnumerator()
                {
                    Selecter = m_selecter,
                };
            }

            class SelectEnumerator : DecorateEnumerator
            {
                public Func<TS, TD> Selecter;

                public override void Dispose()
                {
                    base.Dispose();

                    Selecter = null;
                }

                public override bool MoveNext()
                {
                    if (Source.MoveNext())
                    {
                        Current = Selecter(Source.Current);

                        return true;
                    }

                    return false;
                }
            }
        }

        class ListEnumerable<T> : IEnumerable<T>
        {
            IList<T> m_list;

            public IList<T> List
            {
                set
                {
                    Check.Null(value);

                    m_list = value;
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                var res = new ListEnumerator()
                {
                    List = m_list,
                };

                res.Reset();

                return res;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            class ListEnumerator : IEnumerator<T>
            {
                public IList<T> List;
                int m_currIndex;

                object IEnumerator.Current => List[m_currIndex];

                public T Current => List[m_currIndex];

                public void Dispose()
                {
                    List = null;
                    m_currIndex = -1;
                }

                public bool MoveNext()
                {
                    return ++m_currIndex < List.Count;
                }

                public void Reset()
                {
                    m_currIndex = -1;
                }
            }
        }

        class DistinctEnumerable<T> : DecorateEnumerable<T>
        {
            Func<T, T, bool> m_distiction;

            public Func<T, T, bool> Distinction
            {
                set
                {
                    Check.Null(value);

                    m_distiction = value;
                }
            }

            protected override DecorateEnumerator GetDecorateEnumerator()
            {
                return new DistinctEnumerator()
                {
                    Distinction = m_distiction,
                };
            }

            class DistinctEnumerator : DecorateEnumerator, IEqualityComparer<T>
            {
                public Func<T, T, bool> Distinction;

                HashSet<T> m_set;

                public DistinctEnumerator()
                {
                    m_set = new HashSet<T>(this);
                }

                public override bool MoveNext()
                {
                    while (Source.MoveNext())
                    {
                        var srcCurr = Source.Current;

                        if (m_set.Add(srcCurr))
                        {
                            Current = srcCurr;

                            return true;
                        }
                    }

                    return false;
                }

                public override void Dispose()
                {
                    base.Dispose();

                    Distinction = null;

                    m_set.Clear();
                }

                public override void Reset()
                {
                    base.Reset();

                    m_set.Clear();
                }

                public bool Equals(T x, T y)
                {
                    return Distinction(x, y);
                }

                public int GetHashCode(T obj)
                {
                    return obj.GetHashCode();
                }
            }
        }
    }
}