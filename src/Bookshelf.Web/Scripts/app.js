/**
 * Our main app file with our module declarations and controllers.
 *
 * @author James Huston <james@jameshuston.net>
 * @since 2013-07-18
 */
angular.module('bookshelf', ['bookshelf.services.books'])
    .config(function($routeProvider) {
        $routeProvider
            .when('/book', {
                templateUrl: 'views/book.html',
                controller: 'BookController'
            })
            .when('/book/:bookId', {
                templateUrl: 'views/book.html',
                controller: 'BookController'
            })
            .when('/:filter', {
                templateUrl: 'views/books.html',
                controller: 'BookshelfController'
            })
            .otherwise({
                redirectTo: '/'
            });
    })
    .controller('BookshelfController', function($scope, bookService, $routeParams) {
        var filter = $routeParams.filter || undefined;
        $scope.readFilter = {};
        if (filter === 'reading') {
            $scope.readFilter.Completed = 'false';
        } else if (filter === 'completed') {
            $scope.readFilter.Completed = 'true';
        }

        bookService
            .getBooks()
            .success(function(data, status, headers, config) {
                $scope.bookArray = data;
            });

        $scope.toggleStatus = function(book) {
            book.Completed = !book.Completed;
            bookService
                .updateBook(book.Id, book);
        };

        $scope.removeBook = function(bookId) {
            bookService
                .deleteBook(bookId)
                .success(function() {
                    bookService
                        .getBooks()
                        .success(function(data, status, headers, config) {
                            $scope.bookArray = data;
                        });
                });
        };
    })
    .controller('BookController', function($scope, bookService, $routeParams, $location) {
        $scope.book = {};
        $scope.bookId = $routeParams.bookId || undefined;
        if ($scope.bookId !== undefined) {
            bookService
                .getBooks($scope.bookId)
                .success(function(data, status, headers, config) {
                    $scope.book = data;
                });
        }

        $scope.updateBook = function(bookId) {
            if (bookId !== undefined) {
                bookService
                    .updateBook(bookId, $scope.book);
            } else {
                bookService
                    .addBook($scope.book);
            }
            $location.url('/');
        };

        $scope.cancelUpdate = function() {
            $location.url('/');
        };
    })
    .controller('NavigationController', function($scope, $location) {
        $scope.path = $location.path();
    });