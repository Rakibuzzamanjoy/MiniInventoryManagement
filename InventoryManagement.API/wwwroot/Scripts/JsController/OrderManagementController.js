var app = angular.module("OrdApp", []);

app.controller("OrderManagementController", function ($scope, $http) {

    $scope.OrderList = [];
    $scope.ProductList = [];

    $scope.Order = {};
    $scope.orderItems = [];
    $scope.productInfo = {};


    $http({
        method: 'GET',
        url: '/OrderList'
    }).then(function (response) {
        if (response.data) {
            $scope.OrderList = response.data;
        }
        else {
            alert("No data found!")
        }
    });

    $http({
        method: 'GET',
        url: '/GetProducts'
    }).then(function (response) {
        debugger;
        if (response.data) {

            $scope.ProductList = response.data;

        } else {

            alert('Error: ' + 'No Data Found!');
        }
    });

    $scope.addData = false;
    $scope.addInput = function () {
        $scope.addData = true;     
    }

    $scope.updateUnitPrice = function () {
        if ($scope.productInfo.productId) {
            var selectedProduct = $scope.ProductList.find(function (product) {
                return product.productId == $scope.productInfo.productId;
            });

            if (selectedProduct) {
                $scope.productInfo.unitPrice = selectedProduct.price;
                //  $scope.updateTotalPrice();
            }
        } else {
            $scope.productInfo.unitPrice = 0;
            //$scope.newItem.totalPrice = 0;
        }
    };

    $scope.updateTotalPrice = function () {
        if ($scope.productInfo.quantity && $scope.productInfo.unitPrice) {
            $scope.productInfo.totalPrice = $scope.productInfo.unitPrice * $scope.productInfo.quantity;
        } else {
            $scope.productInfo.totalPrice = 0;
        }
    };

    $scope.getTotalQuantity = function () {
        var total = 0;
        angular.forEach($scope.orderItems, function (item) {
            total += item.quantity;
        });
        return total;
    };
    $scope.calculateOrderTotal = function () {
        var total = 0;
        angular.forEach($scope.orderItems, function (item) {
            total += item.unitPrice * item.quantity;
        });
        return total;
    };

    $scope.addOrderItem = function () {

        $scope.productInfo.productId = $scope.productInfo.productId;
        $scope.productInfo.quantity = $scope.productInfo.quantity;
        $scope.productInfo.unitPrice = $scope.productInfo.unitPrice;
        $scope.productInfo.totalPrice = $scope.productInfo.totalPrice;

        $scope.orderItems.push($scope.productInfo);
        $scope.addData = false;

        $scope.productInfo = {};
    };

    $scope.addOrder = function () {

        $scope.Order = {
            CustomerName: $scope.Order.name,
            OrderItems: $scope.orderItems,
        };
        $http({
            method: 'POST',
            url: '/CreateOrder',
            data: $scope.Order
        }).then(function (response) {
            if (response.data.success) {
                alert(response.data.message);
                window.location.href = "/Home/OrderManagement";

            } else {
                alert('Error: ' + response.data.message);
            }
        })

    };
});