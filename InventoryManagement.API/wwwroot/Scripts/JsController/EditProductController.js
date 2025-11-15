var app = angular.module("EditProd", []);

app.controller("EditProductController", function ($scope, $http) {

    $scope.Productdata = [];
    $scope.Product = {};
    $scope.productForEdit = {};
    $scope.EditedData = {};


    const urlParams = new URLSearchParams(window.location.search);
    const productId = urlParams.get('id');
    $scope.Id = productId;

    $http({
        method: 'GET',
        url: '/GetProductById/' + $scope.Id

    }).then(function (response) {
        if (response.data) {
            $scope.productForEdit = response.data;

        } else {
            alert('Error: ' + 'No Data Found!');
        }
    });

    $scope.saveEditedProduct = function () {
        $scope.Id = productId;
        $scope.EditedData = {
            Name: $scope.productForEdit.name,
            Description: $scope.productForEdit.description,
            Price: $scope.productForEdit.price,
            StockQuantity: $scope.productForEdit.stockQuantity
        };

        $http({
            method: 'PUT',
            url: '/UpdateProduct/' + $scope.Id,
            data: $scope.EditedData
        }).then(function (response) {
            if (response.data) {
                alert(response.data);
                window.location.href = "/Home/ProductManagement";

            } else {
                alert('Error: ' + response.data);
            }
        });
    }
 

});