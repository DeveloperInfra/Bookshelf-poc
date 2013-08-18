/**
 * A book service for working with book data.
 *
 * Ideally this would use something like ng-resource
 * to make restful calls to an api for all of these
 * operations. 
 *
 * @author James Huston <james@jameshuston.net>
 * @since 2013-07-18
 */
angular.module('bookshelf.services.books', [])
    .factory('bookService', [
        '$http',
        function($http) {
            return {
                getBooks: function(bookId) {
                    if (bookId === undefined) {
                        return $http({
                            method: 'GET',
                            url: 'api/book'
                        });
                    }

                    bookId = parseInt(bookId);

                    return $http({
                        method: 'GET',
                        url: 'api/book/' + bookId
                    });
                },

                addBook: function(bookData) {
                    return $http({
                        method: 'POST',
                        url: 'api/book',
                        data: bookData
                    });
                },

                updateBook: function(bookId, bookData) {
                    return $http({
                        method: 'PUT',
                        url: 'api/book/' + bookId,
                        data: bookData
                    });
                },

                deleteBook: function(bookId) {
                    if (bookId === undefined) {
                        bookId = -1;
                    }

                    bookId = parseInt(bookId);

                    return $http({
                        method: 'DELETE',
                        url: 'api/book/' + bookId
                    });
                }
            };
        }
    ]);