(function($) {

    $(document).ready(function() {

        // Initialise the FullPage Layout JS.
        $(".wrapper").fullpage({
            sectionSelector: '.section',
            anchors: ['home', 'gallery', 'form', 'manage']
        });

        $("#name").val(visitor.name);
        $("#phone").val(visitor.phoneNumber);
        $("#email").val(visitor.emailAddress);
        $("#buyer").prop("checked", visitor.isBuyer === 1);
        $("#vendor").prop("checked", visitor.isVendor === 1);
        $("#intouch").prop("checked", visitor.inTouch === 1);

        $("#done").click(function() {
            visitor.name = $("#name").val();
            visitor.phoneNumber = $("#phone").val();
            visitor.emailAddress = $("#email").val();
            visitor.isBuyer = $("#buyer").prop("checked") ? 1 : 0;
            visitor.isVendor = $("#vendor").prop("checked") ? 1 : 0;
            visitor.inTouch = $("#intouch").prop("checked") ? 1 : 0;
            console.log(visitor);

            $.ajax({
                url: "/visitors/",
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(visitor),
                processData: false,
                success:function(data) {
                    console.log(data);
                    $("#who").click();
                    window.location = "/dummy#manage";
                },
                error:function(xhr, ts, et) {
                    console.log(ts);
                }
            });
        });

        $("#notify").click(function() {
            $.ajax({
                url: "/visitors/notifications",
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                processData: false,
                success:function(data) {
                    console.log(data);
                },
                error:function(xhr, ts, et) {
                    console.log(ts);
                }
            });
        });

        $("#who").click(function() {
            $.ajax({
                url: "/visitors/",
                type: "GET",
                processData: false,
                success:function(data) {
                    console.log(data);
                    $('#visitors tbody').empty();
                    for (var i in data) {
                        $('#visitors tbody').append(
                        '<tr>' +
                        '<td>' + data[i].name + '</td>' +
                        '<td>' + data[i].phoneNumber + '</td>' +
                        '<td>' + data[i].emailAddress + '</td>' +
                        '<td>' + (data[i].isBuyer === 1 ? 'Yes' : 'No') + '</td>' +
                        '<td>' + (data[i].isVendor === 1 ? 'Yes' : 'No') + '</td>' +
                        '</tr>');
                    }
                },
                error:function(xhr, ts, et) {
                    console.log(ts);
                }
            });
        });

        $("#who").click();

    });
})(jQuery);