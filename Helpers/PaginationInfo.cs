// <copyright file="PaginationInfo.cs" company="Tom Cook">
//   Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Helpers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  /// <summary>
  /// Provides properties and methods that aid the creation of paginated collections.
  /// </summary>
  public class PaginationInfo
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    public PaginationInfo()
      : this(0, 1)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    /// <param name="numberOfItems">The number of items.</param>
    public PaginationInfo(int numberOfItems)
      : this(numberOfItems, 1)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="numberOfItems">The number of items.</param>
    public PaginationInfo(int numberOfItems, int pageNumber)
      : this(numberOfItems, pageNumber, 20)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginationInfo"/> class.
    /// </summary>
    /// <param name="itemsPerPage">The number of items per page.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="numberOfItems">The number of items.</param>
    public PaginationInfo(int numberOfItems, int pageNumber, int itemsPerPage)
    {
      this.NumberOfItems = numberOfItems;

      if (itemsPerPage > 0)
      {
        this.ItemsPerPage = itemsPerPage;
      }
      else
      {
        this.ItemsPerPage = 20;
      }

      if (pageNumber > 0 && pageNumber <= this.NumberOfPages)
      {
        this.PageNumber = pageNumber;
      }
      else
      {
        this.PageNumber = 1;
      }
    }

    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the number of items.
    /// </summary>
    public int NumberOfItems { get; set; }

    /// <summary>
    /// Gets or sets the number of items per page.
    /// </summary>
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// Gets the first item number on ths page.
    /// </summary>
    public int FirstItemNumber
    {
      get
      {
        return 1 + ((this.PageNumber - 1) * this.ItemsPerPage);
      }
    }

    /// <summary>
    /// Gets the last item number on the page.
    /// </summary>
    public int LastItemNumber
    {
      get
      {
        return Math.Min(
          this.FirstItemNumber + this.ItemsPerPage - 1, this.NumberOfItems);
      }
    }

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

        return (int)Math.Ceiling(
          (float)this.NumberOfItems / this.ItemsPerPage);
      }
    }

    /// <summary>
    /// Gets a value indicating whether there is a previous page number or not.
    /// </summary>
    public bool HasPreviousPageNumber
    {
      get
      {
        return !(this.PreviousPageNumber == this.PageNumber);
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
    /// Gets a value indicating whether there is a next page number or not.
    /// </summary>
    public bool HasNextPageNumber
    {
      get
      {
        return !(this.NextPageNumber == this.PageNumber);
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
    public IEnumerable<int> PageNumberList
    {
      get
      {
        var pages =
          Enumerable
            .Range(this.PageNumber - 5, 10)
            .Where(n => n >= 1 && n <= this.NumberOfPages);

        var distance = this.NumberOfPages - this.PageNumber;

        if (distance > 1)
        {
          pages = pages.Where(p => p >= this.PageNumber - 2);
        }
        else
        {
          pages = pages.Where(p => p >= this.PageNumber - (4 - distance));
        }

        return pages.Take(5);
      }
    }

    /// <summary>
    /// Gets the number of items to skip.
    /// </summary>
    /// <returns>The number of items to skip.</returns>
    public int GetNumberOfItemsToSkip()
    {
      return (this.PageNumber - 1) * this.ItemsPerPage;
    }
  }
}
