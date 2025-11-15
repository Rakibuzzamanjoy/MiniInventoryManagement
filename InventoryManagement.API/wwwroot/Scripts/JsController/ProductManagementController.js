var app = angular.module("Prodapp", []);

app.controller("ProductManagementController", function ($scope, $http) {

    $scope.Productdata = [];
    $scope.Product = {};
    $scope.productForEdit = {};

    $http({
        method: 'GET',
        url: '/GetProducts'
    }).then(function (response) {
        debugger;
        if (response.data) {

            $scope.Productdata = response.data;

        } else {

            alert('Error: ' + 'No Data Found!');
        }
    });

    $scope.addProduct = function () {
        debugger;
        $scope.product = {
            Name: $scope.Product.name,
            Description: $scope.Product.description,
            Price: $scope.Product.price,
            StockQuantity: $scope.Product.stock,

        };

        $http({
            method: 'POST',
            url: '/CreateProduct',
            data: $scope.product
        }).then(function (response) {
            if (response.data.success) {
                alert(response.data.message);
                $scope.clearForm();

            } else {
                alert('Error: ' + response.data.message);
            }
        })
    };

    $scope.clearForm = function () {
        $scope.Product = {
            name : '',
            description: '',
            price: '',
            stock : '',

        };
    };

    $scope.editProduct = function (productId) {

        window.location.href = "/Home/EditProduct?id=" + productId;
        
    };
    $scope.deleteProduct = function (productId) {

        $http({
            method: 'DELETE',
            url: '/RemoveProduct/' + productId
            
        }).then(function (response) {
            if (response.data) {
                alert(response.data);
                window.location.href = "/Home/ProductManagement";

            } else {
                alert('Error: ' + response.data);
            }
        });
        
    };
  
});