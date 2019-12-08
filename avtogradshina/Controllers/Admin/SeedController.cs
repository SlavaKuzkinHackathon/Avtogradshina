using avtogradshina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using avtogradshina.Models.Admin;

namespace avtogradshina.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    public class SeedController : Controller
    {
        private ApplicationContext context;

        public SeedController(ApplicationContext ctx) => context = ctx;

        public IActionResult Index()
        {
            ViewBag.Count = context.Products.Count();
            return View(context.Products
                .Include(p => p.Category).OrderBy(p => p.Id).Take(20));
        }

        [HttpPost]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
                context.Database
                    .ExecuteSqlCommand("DROP PROCEDURE IF EXISTS CreateSeedData");
                context.Database.ExecuteSqlCommand($@"
                    CREATE PROCEDURE CreateSeedData
	                    @RowCount decimal
                    AS
	                  BEGIN
	                  SET NOCOUNT ON
                      DECLARE @i INT = 1;
	                  DECLARE @catId BIGINT;
	                  DECLARE @CatCount INT = @RowCount / 10;
	                  DECLARE @price INT = 1;
	                  DECLARE @inquantity INT = 1;
	                  BEGIN TRANSACTION
		                WHILE @i <= @CatCount
			              BEGIN
				            INSERT INTO Categories (Name)
				            VALUES (CONCAT('Category-', @i));
				            SET @catId = SCOPE_IDENTITY();
				            DECLARE @j INT = 1;
				            WHILE @j <= 10
					        BEGIN
						   SET @price = RAND()*(500-5+1);
						   SET @inquantity = RAND()*(500-5+1);
						   INSERT INTO Products (Name, CategoryId, 
                                                    Price, InQuantity)
						   VALUES (CONCAT('Product', @i, '-', @j), 
                                                 @catId, @price, @inquantity)
						   SET @j = @j + 1
					          END
		                    SET @i = @i + 1
		                    END
	                    COMMIT
                    END");
                context.Database.BeginTransaction();
                context.Database
                    .ExecuteSqlCommand($"EXEC CreateSeedData @RowCount = {count}");
                context.Database.CommitTransaction();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearData()
        {
            context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
            context.Database.BeginTransaction();
            context.Database.ExecuteSqlCommand("DELETE FROM Orders");
            context.Database.ExecuteSqlCommand("DELETE FROM Categories");
            context.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CreateProductionData()
        {
            ClearData();

            context.Categories.AddRange(new Category[] {
                new Category {
                    Name = "Автошины",
                    Products = new Product[] {
                        new Product {
                            Name = "Автошина 175/70R14 MAXXIS NP-5 84T шип зимняя",
                            Price = 2970, InQuantity = 4, Path = "/Files/Nexen ru1.jpg"
                        },
                        new Product {
                            Name = "Автошина 195/65R15 Gislaved Nord Frost 200 ID 95T  шип  зимняя",
                            Price = 3830, InQuantity = 4, Path = "/Files/Nexen ru1.jpg"
                        },
                        new Product {
                            Name = "Автошина 215/70R16 Nitto Therma Spike 100T шип зимняя",
                            Price = 6840, InQuantity = 4, Path = "/Files/Nexen ru1.jpg"
                        },
                        new Product {
                            Name = "Автошина 215/45R17    Hankook  W429  91T  XL  шип",
                            Price = 7450, InQuantity = 17, Path = "/Files/Nexen ru1.jpg"
                        },
                         new Product {
                            Name = "Автошина 215/60R16 95Q Nexen Win-Ice всесезонная",
                            Price = 4670, InQuantity = 4, Path = "/Files/Nexen ru1.jpg"
                        },
                        new Product {
                            Name = "Автошина 215/60R16 95Q Nexen Win-Ice всесезонная",
                            Price = 4670, InQuantity = 4, Path = "/Files/Nexen ru1.jpg"
                        },
                        new Product {
                            Name = "Автошина 205/65R15 Yokohama ES32  99H  летняя",
                            Price = 3680, InQuantity = 4, Path = "/Files/Nexen ru1.jpg"
                        },
                        new Product {
                            Name = "Автошина 175/70R13 Kama-205 Б/К НК летняя",
                            Price = 1800, InQuantity = 3, Path = "/Files/Nexen ru1.jpg"
                        },
                        new Product {
                            Name = "Автошина 215/70R16 100H Classe Premiere CP671 Nexen  всесезонная",
                            Price = 4930, InQuantity = 4, Path = "/Files/Nexen ru1.jpg"
                        },
                    }
                },
                new Category {
                    Name = "Диски",
                    Products = new Product[] {
                        new Product {
                            Name = "Диск 15 K&K  KC861 Solaris2  6.0*15 4*100 ET46 D54.1 силвер",
                            Price = 4150, InQuantity = 4, Path ="/Files/b012.jpg"
                        },
                        new Product {
                            Name = "Диск 15 Trebl X40923 6.0*15 4*100 ET46 54.1 BK",
                            Price = 1900, InQuantity = 4, Path ="/Files/b012.jpg"
                        },
                        new Product {
                            Name = "Диск 16 K&K  Борелли-оригинал KC613 6.5*16 5*114.3 ET46 D67.1 дарк платинум",
                            Price = 5350,  InQuantity = 4, Path ="/Files/b012.jpg"
                        },
                        new Product {
                            Name = "Диск 15 Cross Street  Y737  6.0*15 5*100 ET38  57.1 S",
                            Price = 3370,  InQuantity = 4, Path ="/Files/b012.jpg"
                        },
                        new Product {
                            Name = "Диск 14 Trebl 53A49A P 5.5*14 4*100 ET49 56.6 BK",
                            Price = 1370,  InQuantity = 4, Path ="/Files/b012.jpg"
                        }
                    }
                },
                new Category {
                    Name = "Аккумуляторы",
                    Products = new Product[] {
                        new Product {
                            Name = "Аккумулятор AKOM-Евро 6CT-75-EFB о.п.  увелич. мощность",
                            Price = 6100, InQuantity = 1, Path = "/Files/catalog_179.jpg"
                        },
                        new Product {
                            Name = "MAXXIS IES  65R-620   56514 пр",
                            Price = 5421, InQuantity = 4, Path = "/Files/catalog_179.jpg"
                        },
                        new Product {
                            Name = "Аккумулятор FB SUPER NOVA 65 75D23R",
                            Price = 7570, InQuantity = 1, Path = "/Files/catalog_179.jpg"
                        },
                        new Product {
                            Name = "Аккумулятор G&YU 90D26R",
                            Price = 7550, InQuantity = 2, Path = "/Files/catalog_179.jpg"
                        },
                        new Product {
                            Name = "MAXXIS JIS  45 (60B24LS)-430 обр",
                            Price = 3250, InQuantity = 21, Path = "/Files/catalog_179.jpg"
                        },
                        new Product {
                            Name = "MAXXIS JIS  45 (60B24R)-430 узк пр",
                            Price = 3250, InQuantity = 11, Path = "/Files/catalog_179.jpg"
                        }
                        
                    }
                }
            });
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}