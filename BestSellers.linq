<Query Kind="Expression">
  <Connection>
    <ID>605c6cf5-a31a-427b-8c54-bb5244f3ecbf</ID>
    <Persist>true</Persist>
    <Server>localhost</Server>
    <Database>DotNetShopping</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Output>DataGrids</Output>
</Query>

Orders
.Join(OrderProducts, o => o.OrderId, op => op.OrderId, (o, op) => new { Order = o, OrderProduct = op })
.Join(Variants, oop => oop.OrderProduct.VariantId, v => v.VariantId, (oop, v) => new { oop, Variant = v })
.Join(Products, oopv => oopv.Variant.ProductId, p => p.ProductId, (oopv, p) => new { oopv, Product = p })
.Join(Categories, oopvp => oopvp.Product.CategoryId, c => c.CategoryId, (oopvp, c) => new { oopvp, Category = c })
.Join(Brands, oopvpc => oopvpc.oopvp.Product.BrandId, b => b.BrandId, (oopvpc, b) => new { oopvpc, Brand = b })
.Where(x => x.oopvpc.oopvp.oopv.Variant.Archived == false &&
x.oopvpc.oopvp.oopv.Variant.IsVisible == true &&
x.oopvpc.oopvp.Product.Archived == false &&
x.oopvpc.oopvp.Product.IsVisible == true &&
x.oopvpc.oopvp.Product.OnSale == true &&
x.oopvpc.oopvp.oopv.Variant.Stock > 0 &&
x.oopvpc.oopvp.oopv.oop.Order.OrderDate > DateTime.Now.AddDays(-30)
)
.GroupBy(x => new { 
	ProductId = x.oopvpc.oopvp.Product.ProductId, 
	VariantId = x.oopvpc.oopvp.oopv.Variant.VariantId,
	ProductName = x.oopvpc.oopvp.oopv.Variant.Product.Name, 
	VariantName = x.oopvpc.oopvp.oopv.Variant.Name,
	BrandName = x.Brand.Name, 
	CategoryName = x.oopvpc.Category.Name,
	UnitPrice = x.oopvpc.oopvp.oopv.Variant.UnitPrice
})
.Select(group => new 
    {
        ProductBox = group.Key,
        Sold = group.Sum(x => x.oopvpc.oopvp.oopv.oop.OrderProduct.Quantity)
    })
.Select(x => new 
    {
        ProductId = x.ProductBox.ProductId,
        VariantId = x.ProductBox.VariantId,
        ProductName = x.ProductBox.ProductName,
        VariantName = x.ProductBox.VariantName,
        BrandName = x.ProductBox.BrandName,
        CategoryName = x.ProductBox.CategoryName,
        UnitPrice = x.ProductBox.UnitPrice,
        Sold = x.Sold
    })
.OrderByDescending(x => x.Sold)
.Take(12)
.Select(x => new 
{
    ProductId = x.ProductId,
    VariantId = x.VariantId,
    ProductName = x.ProductName,
    VariantName = x.VariantName,
    BrandName = x.BrandName,
    CategoryName = x.CategoryName,
    UnitPrice = x.UnitPrice,
	Sold = x.Sold,
    PhotoName = ProductImages
    .Where(i => i.VariantId == x.VariantId)
    .OrderBy(i => i.Sequence).FirstOrDefault().FileName
})
	