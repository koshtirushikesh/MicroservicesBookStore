﻿using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json.Linq;
using System.Net;

namespace BookStore.Order.Service
{
    public class WishListService : IWishListService
    {
        private readonly OrderDBContext orderDBContext;
        private readonly IBookServices bookService;
        private readonly IUserServices userServices;
        public WishListService(OrderDBContext orderDBContext, IBookServices bookService, IUserServices userServices)
        {
            this.orderDBContext = orderDBContext;
            this.bookService = bookService;
            this.userServices = userServices;
        }

        public async Task<WishListEntity> AddWishList(int bookID, int userID, string token)
        {
            if (!orderDBContext.wishList.Any(x => x.UserID == userID && x.BookID == bookID))
            {
                WishListEntity wishList = new WishListEntity()
                {
                    BookID = bookID,
                    UserID = userID,
                    Book = await bookService.GetBookDetails(bookID),
                    User = await userServices.GetUserDetails(token)
                };
                orderDBContext.wishList.Add(wishList);
                orderDBContext.SaveChanges();
                return wishList;
            }
            return null;
        }

        public bool RemoveWishList(int bookID, int userID)
        {
            var result = orderDBContext.wishList.FirstOrDefault(x => x.BookID == bookID && x.UserID == userID);
            if (result != null)
            {
                orderDBContext.wishList.Remove(result);
                orderDBContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<WishListEntity>> GetWishListByUserID(int userID)
        {
            IEnumerable<WishListEntity> wishList = (IEnumerable<WishListEntity>)orderDBContext.wishList.Where(x => x.UserID == userID);
            if (wishList != null)
            {
                foreach(WishListEntity wish in wishList)
                {
                    wish.Book = await bookService.GetBookDetails(wish.BookID);
                    
                }
                return wishList;
            }
            return null;
        }

    }
}
