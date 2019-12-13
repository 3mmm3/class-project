using _3M.ViewModels;
using _3M.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Helpers
{
    public class PagingHelper
    {
        public IList<PagingItem> GetPagings(int current, int count)
        {
            const int itemsCount = 10;
            var results = new List<PagingItem>();
            if (current < 1) current = 1;
            if (current > count) current = count;

            if (count <= itemsCount)
            {
                for (int i = 1; i <= count; i++)
                {
                    results.Add(new PagingItem()
                    {
                        Display = i.ToString(),
                        PageNumber = i,
                        Active = current == i,
                    });
                }

                return results;
            }
            if (current < itemsCount / 2)
            {
                for (int i = 1; i <= itemsCount - 2; i++)
                {
                    results.Add(new PagingItem()
                    {
                        Display = i.ToString(),
                        PageNumber = i,
                        Active = current == i,
                    });
                }
                results.Add(new PagingItem()
                {
                    Display = "...",
                    PageNumber = (count - current) / 2,
                    Active = false,
                });
                results.Add(new PagingItem()
                {
                    Display = count.ToString(),
                    PageNumber = count,
                    Active = false,
                });
                return results;
            }
            if (current > count - itemsCount / 2)
            {
                results.Add(new PagingItem()
                {
                    Display = 1.ToString(),
                    PageNumber = 1,
                    Active = false,
                });
                results.Add(new PagingItem()
                {
                    Display = "...",
                    PageNumber = current / 2,
                    Active = false,
                });

                for (int i = count - (itemsCount - 3); i <= count; i++)
                {
                    results.Add(new PagingItem()
                    {
                        Display = i.ToString(),
                        PageNumber = i,
                        Active = current == i,
                    });
                }

                return results;
            }

            results.Add(new PagingItem()
            {
                Display = 1.ToString(),
                PageNumber = 1,
                Active = false,
            });
            results.Add(new PagingItem()
            {
                Display = "...",
                PageNumber = current / 2,
                Active = false,
            });
            for (int i = current - (itemsCount / 2) + 2; i <= current + (itemsCount / 2) - 2; i++)
            {
                results.Add(new PagingItem()
                {
                    Display = i.ToString(),
                    PageNumber = i,
                    Active = current == i,
                });
            }
            results.Add(new PagingItem()
            {
                Display = "...",
                PageNumber = current + (count - current) / 2,
                Active = false,
            });
            results.Add(new PagingItem()
            {
                Display = count.ToString(),
                PageNumber = count,
                Active = false,
            });
            return results;
        }
    }
}
