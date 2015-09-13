// initialize client with app credentials
SC.initialize({
    client_id: 'ee44893c750107d61ba4e173679d27f8',
    redirect_uri: 'http://localhost/CrystalVortex/'
});

// initiate auth popup
SC.connect(function () {
    SC.get('/me', function (me) {
        alert('Hello, ' + me.username);
    });
});