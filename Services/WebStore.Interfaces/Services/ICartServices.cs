﻿using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface ICartServices
    {
        void Add(int id);

        void Decrement(int id);

        void Remove(int id);

        void Clear();

        CartViewModel GetViewModel();
    }
}
