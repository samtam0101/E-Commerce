using Domain.DTOs.ProductDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<Response<string>> AddProductAsync(AddProductDto addProductDto);
    Task<Response<string>> UpdateProductAsync(UpdateProductDto updateProductDtop);
    Task<Response<bool>> DeleteProductAsync(int id);
    Task<PagedResponse<List<GetProductsDto>>> GetProductsAsync(ProductFilter filter);
    Task<Response<GetProductsDto>> GetProductByIdAsync(int id);
}
/*
POST /api/orders
Creates a new order.
Get All Orders (Admin)

GET /api/orders
Retrieves a list of all orders.
Get Orders by User

GET /api/orders/user/{userId}
Retrieves all orders for a specific user.
Get Order by ID

GET /api/orders/{id}
Retrieves a specific order by its ID.
Update Order Status (Admin)

PUT /api/orders/{id}/status
Updates the status of an order.
Shopping Cart APIs
Add Item to Cart

POST /api/cart
Adds an item to the shopping cart.
Get Cart Items

GET /api/cart
Retrieves all items in the shopping cart.
Update Cart Item

PUT /api/cart/{itemId}
Updates the quantity of a specific item in the cart.
Remove Cart Item

DELETE /api/cart/{itemId}
Removes an item from the cart.
Wishlist APIs
Add Item to Wishlist

POST /api/wishlist
Adds an item to the wishlist.
Get Wishlist Items

GET /api/wishlist
Retrieves all items in the wishlist.
Remove Wishlist Item

DELETE /api/wishlist/{itemId}
Removes an item from the wishlist.
Review APIs
Add Product Review

POST /api/reviews
Adds a review for a product.
Get Reviews for Product

GET /api/reviews/product/{productId}
Retrieves all reviews for a specific product.
Delete Review (Admin)

DELETE /api/reviews/{reviewId}
Deletes a specific review.
Payment APIs
Create Payment

POST /api/payments
Processes a payment for an order.
Get Payment by Order

GET /api/payments/order/{orderId}
Retrieves the payment details for a specific order.
Address Management APIs
Add Address

POST /api/addresses
Adds a new address for the user.
Get Addresses

GET /api/addresses
Retrieves all addresses for the logged-in user.
Update Address

PUT /api/addresses/{id}
Updates a specific address.
Delete Address

DELETE /api/addresses/{id}
Deletes a specific address.
*/