$(document).ready(function () {
    

    var ajaxcall = function () {
        var id = $('#ItemID').val();
        var query = "Auction/GetBids?id=" + id;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: query,
            success: loadBids,
            error: failed
        });
    };


    function loadBids(data) {
        var temp = JSON.parse(data);
        $('.bids').empty();
        $('.bids').append('<tr> <th> Bidder Name </th><th>Bidder Price</th></tr>');
        for (var i = 0; i < temp.length; i += 1) {
            $('.bids').append('<tr> <td>' + temp[i].Buyer + '</td><td> ' + temp[i].Price + '</td></tr>');
        
    }

    function failed() {

        }

    var interval = 5000;
    window.setInterval(ajaxcall, interval);
});