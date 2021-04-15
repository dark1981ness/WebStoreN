using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<WebStoreDbInitializer> _logger;

        public WebStoreDbInitializer(WebStoreDB db,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILogger<WebStoreDbInitializer> logger)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public void Initialize()
        {
            _logger.LogInformation("Инициализация БД...");

            //_db.Database.EnsureDeleted();
            //_db.Database.EnsureCreated();

            if (_db.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Выполнение миграции БД...");
                _db.Database.Migrate();
                _logger.LogInformation("Выполнение миграции БД выполнено успешно");
            }

            try
            {
                InitializeProducts();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ошибка при инициализации товаров в БД");
                throw;
            }

            try
            {
                InitializeIdentityAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ошибка при инициализации данных системы Identity");
                throw;
            }

            _logger.LogInformation("Инициализация БД выполнена успешно");
        }

        private void InitializeProducts()
        {

            if (_db.Products.Any())
            {
                _logger.LogInformation("Инициализация товаров не нужна");
                return;
            }

            _logger.LogInformation("Инициализация секций...");

            var products_sections = TestData.Sections.Join(
                TestData.Products,
                section => section.Id,
                product => product.SectionId,
                (section, product) => (section, product));

            foreach (var (section, product) in products_sections)
                section.Products.Add(product);

            var products_brands = TestData.Brands.Join(
                TestData.Products,
                brand => brand.Id,
                product => product.BrandId,
                (brand, product) => (brand, product));

            foreach (var (brand, product) in products_brands)
                brand.Products.Add(product);

            var section_section = TestData.Sections.Join(
                TestData.Sections,
                parent => parent.Id,
                child => child.ParentId,
                (parent, child) => (parent, child));

            foreach (var (parent, child) in section_section)
                child.Parent = parent;

            foreach (var product in TestData.Products)
            {
                product.Id = 0;
                product.BrandId = null;
                product.SectionId = 0;
            }

            foreach (var brand in TestData.Brands)
                brand.Id = 0;

            foreach (var section in TestData.Sections)
            {
                section.Id = 0;
                section.ParentId = null;
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(TestData.Products);
                _db.Sections.AddRange(TestData.Sections);
                _db.Brands.AddRange(TestData.Brands);

                _db.SaveChanges();
                _db.Database.CommitTransaction();
            }

            _logger.LogInformation("Инициализация товаров завершена");
        }

        private async Task InitializeIdentityAsync()
        {
            _logger.LogInformation("Инициализация БД системы Identity");
            async Task CheckRole(string roleName)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    _logger.LogInformation("Роль {0} отсутствуетю Создаю...", roleName);
                    await _roleManager.CreateAsync(new Role { Name = roleName });
                    _logger.LogInformation("Роль {0} создана успешно", roleName);
                }
            }

            await CheckRole(Role._administrators);
            await CheckRole(Role._users);

            if (await _userManager.FindByNameAsync(User._administrator) is null)
            {
                _logger.LogInformation("Учетная запись администратора в БД отсутствует. Создаю...");

                var admin = new User
                {
                    UserName = User._administrator
                };

                var creation_result = await _userManager.CreateAsync(admin, User._defaultAdminPassword);

                if (creation_result.Succeeded)
                {
                    _logger.LogInformation("Учетная запись администратора создана успешно");
                    await _userManager.AddToRoleAsync(admin, Role._administrators);
                    _logger.LogInformation("Учетная запись администратора наделена ролью администартора");
                }
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description).ToArray();
                    _logger.LogInformation("Учетная запись администратора создана с ошибкой{0}", string.Join(",", errors));
                    throw new InvalidOperationException($"Оштбка при создании учетной записи администратора: {string.Join(",", errors)}");
                }
            }

            _logger.LogInformation("Инициализация БД системы Identity выполнена");
        }

    }
}
