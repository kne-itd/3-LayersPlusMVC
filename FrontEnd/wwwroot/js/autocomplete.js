    $( function() {
    var availableZips = [];
        $.ajax({
            url: "https://api.dataforsyningen.dk/postnumre"
        }).done(function (data) {
            $.each(data, function (index, value) {
                availableZips.push(value.nr + " " + value.navn);
            });
            $(this).addClass("done");
            console.log(availableZips);
        });
    $( "#zip" ).autocomplete({
        source: availableZips
    });
  } );