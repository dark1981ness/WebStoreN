using System;
using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO
{
    /// <summary>
    /// Информация о заказе
    /// </summary>
    public class OrderDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя заказа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Телефон, указанный в заказе для связи
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Перечень пунктов заказа
        /// </summary>
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }

    /// <summary>
    /// Пункт заказа
    /// </summary>
    public class OrderItemDTO
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Quantity { get; set; }
    }

    /// <summary>
    /// Модель процесса создания заказа
    /// </summary>
    public class CreateOrderModel
    {
        /// <summary>
        /// Модель заказа
        /// </summary>
        public OrderViewModel Order { get; set; }

        /// <summary>
        /// Перечень элементов заказа
        /// </summary>
        public IList<OrderItemDTO> Items { get; set; }
    }
}
