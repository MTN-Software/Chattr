var ViewModel = function () {
    var self = this;
    self.messages = ko.observableArray(); // holds list of messages
    self.error = ko.observable();
    self.users = ko.observableArray();
    self.newMessage = {
        User: ko.observable(),
        Content: ko.observable()
    }
    self.newUser = {
        Name: ko.observable()
    }

    var usersUri = '/api/Users';
    var messagesUri = '/api/Messages';

    function ajaxHelper(uri, method, data) {
        self.error(''); // clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });

        //ko.viewModel.updateModel(this, data.d);
    }

    function getAllMessages() {
        ajaxHelper(messagesUri, 'GET').done(function (data) {
            self.messages(data);
        });
    }

    function getAllUsers() {
        ajaxHelper(usersUri, 'GET').done(function (data) {
            self.users(data);
        });
    }

    self.addMessage = function (formElement) {
        var message = {
            UserID: self.newMessage.User().ID,
            Content: self.newMessage.Content()
        };

        ajaxHelper(messagesUri, 'POST', message).done(function (item) {
            self.messages.push(item);
        });
    }

    self.addUser = function (formElement) {
        var user = {
            Name: self.newUser.Name()
        };

        ajaxHelper(usersUri, 'POST', user).done(function (item) {
            self.users.push(item);
        });
    }

    // fetch the initial data
    getAllMessages();
    getAllUsers();
};

ko.applyBindings(new ViewModel());