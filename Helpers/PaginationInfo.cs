using System;
using System.Collections.Generic;
using System.Linq;

namespace MintyPeterson.Helpers
{
  /// <summary>
  /// Provides properties and methods that aid the creation of paginated collections.
  /// </summary>
  public class PaginationInfo
  {
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int PageNumber { get; private set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int ItemsPerPage { get; private set; }

    /// <summary>
    /// Gets or sets the number of items.
    /// </summary>
    public int NumberOfItems { get; private set; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int NumberOfPages
    {
      get
      {
        if (this.NumberOfItems == 0)
        {
          return 1;
        }

        return (int)Math.Ceiling((float)this.NumberOfItems / this.ItemsPerPage);
      }
    }

    /// <summary>
    /// Gets the number of items to skip.
    /// </summary>
    public int NumberOfItemsToSkip
    {
      get
      {
        return (this.PageNumber - 1) * this.ItemsPerPage;
      }
    }

    /// <summary>
    /// Gets the previous page number.
    /// </summary>
    public int PreviousPageNumber
    {
      get
      {
        if (this.PageNumber <= 1)
        {
          return 1;
        }

        return this.PageNumber - 1;
      }
    }

    /// <summary>
    /// Gets the next page number.
    /// </summary>
    public int NextPageNumber
    {
      get
      {
        var nextPage = this.PageNumber + 1;

        if (nextPage > this.NumberOfPages)
        {
          nextPage = this.NumberOfPages;
        }

        return nextPage;
      }
    }

    /// <summary>
    /// Gets an enumerable list of page numbers in short format.
    /// </summary>
    public IEnumerable<int> ShortPageList
    {
      get
      {
        var pages =
          Enumerable
            .Range(
              this.PageNumber - 5, 10
            )
            .Where(
              n => n >= 1 && n <= this.NumberOfPages
            );

        var distance = this.NumberOfPages - this.PageNumber;

        if (distance > 1)
        {
          pages = pages.Where(p => p >= this.PageNumber - 2);
        }
        else
        {
          pages =
            pages.Where(
              p => p >= this.PageNumber - (4 - distance)
            );
        }

        return pages.Take(5);
      }
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    public PaginationInfo(int numberOfItems)
      : this(numberOfItems, 1)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    public PaginationInfo(int numberOfItems, int pageNumber)
      : this(numberOfItems, pageNumber, 20)
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    /// <param name="itemsPerPage">The number of items per page.</param>
    /// <param name="numberOfItems">The number of items.</param>
    /// <param name="page">The page number.</param>
    public PaginationInfo(int numberOfItems, int pageNumber, int itemsPerPage)
    {
      this.NumberOfItems = numberOfItems;
      this.ItemsPerPage = itemsPerPage;

      if (pageNumber > 0 && pageNumber <= this.NumberOfPages)
      {
        this.PageNumber = pageNumber;
      }
      else
      {
        this.PageNumber = 1;
      }
    }
  }
}
