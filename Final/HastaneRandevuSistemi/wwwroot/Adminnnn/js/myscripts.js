$(document).ready(function () {
    $('#selectedsehir').attr('disabled', true);
    $('#selectedilce').attr('disabled', true);
    $('#selectedhastane').attr('disabled', true);
    $('#selectedbolum').attr('disabled', true);
    $('#selecteddoktor').attr('disabled', true);
    $('#selectedgun').attr('disabled', true);
    $('#selectedsaat').attr('disabled', true);
    LoadSehirler();
    var hastaneidsi;
    $('#selectedsehir').change(function () {
        var ilceidsi = $(this).val();
        console.log(ilceidsi);
        if (ilceidsi > 0) {
            Loadilceler(ilceidsi);
        }
        else {
            alert("Sehir secin!");
            $('#selectedilce').empty();
            $('#selectedhastane').empty();
            $('#selectedbolum').empty();
            $('#selecteddoktor').empty();
            $('#selectedgun').empty();
            $('#selectedsaat').empty();
            $('#selectedilce').attr('disabled', true);
            $('#selectedhastane').attr('disabled', true);
            $('#selectedbolum').attr('disabled', true);
            $('#selecteddoktor').attr('disabled', true);
            $('#selectedgun').attr('disabled', true);
            $('#selectedsaat').attr('disabled', true);
            $('#selectedilce').append('<option>--Ilce secin--</option>');
            $('#selectedhastane').append('<option>--Hastane secin--</option>');
            $('#selectedbolum').append('<option>--Bolum secin--</option>');
            $('#selecteddoktor').append('<option>--Doktor secin--</option>');
            $('#selectedgun').append('<option>--Gun secin--</option>');
            $('#selectedsaat').append('<option>--Saat secin--</option>');
        }
    });

    $('#selectedilce').change(function () {
        var sehiridsi = $(this).val();
        if (sehiridsi > 0) {
            Loadhastaneler(sehiridsi);
        }
        else {
            alert("Hastane secin!");
            $('#selectedhastane').empty();
            $('#selectedbolum').empty();
            $('#selecteddoktor').empty();
            $('#selectedgun').empty();
            $('#selectedsaat').empty();
            $('#selectedhastane').attr('disabled', true);
            $('#selectedbolum').attr('disabled', true);
            $('#selecteddoktor').attr('disabled', true);
            $('#selectedgun').attr('disabled', true);
            $('#selectedsaat').attr('disabled', true);
            $('#selectedhastane').append('<option>--Hastane secin--</option>');
            $('#selectedbolum').append('<option>--Bolum secin--</option>');
            $('#selecteddoktor').append('<option>--Doktor secin--</option>');
            $('#selectedgun').append('<option>--Gun secin--</option>');
            $('#selectedsaat').append('<option>--Saat secin--</option>');
        }
    });

    $('#selectedhastane').change(function () {   
        hastaneidsi = $(this).val();
        console.log(hastaneidsi);
        Loadbolumler();
        
    });


    $('#selectedbolum').change(function () {
        var bolumidsi = $(this).val();
       // hastaneidsi = $(this).val();
        if (bolumidsi > 0 && hastaneidsi > 0) {
            Loaddoktorlar(bolumidsi, hastaneidsi);
        }
        else {
            alert("Hastane secin!");
            $('#selectedhastane').empty();
            $('#selectedbolum').empty();
            $('#selecteddoktor').empty();
            $('#selectedgun').empty();
            $('#selectedsaat').empty();
            $('#selectedhastane').attr('disabled', true);
            $('#selectedbolum').attr('disabled', true);
            $('#selecteddoktor').attr('disabled', true);
            $('#selectedgun').attr('disabled', true);
            $('#selectedsaat').attr('disabled', true);
            $('#selectedhastane').append('<option>--Hastane secin--</option>');
            $('#selectedbolum').append('<option>--Bolum secin--</option>');
            $('#selecteddoktor').append('<option>--Doktor secin--</option>');
            $('#selectedgun').append('<option>--Gun secin--</option>');
            $('#selectedsaat').append('<option>--Saat secin--</option>');
        }
    });

    $('#selecteddoktor').change(function () {
        var doktoridsi = $(this).val();
        
        if (doktoridsi > 0) {
            Loadgunler(doktoridsi);
        }
        else {
            alert("Doktor Seçin!");
            $('#selectedhastane').empty();
            $('#selectedbolum').empty();
            $('#selecteddoktor').empty();
            $('#selectedgun').empty();
            $('#selectedsaat').empty();
            $('#selectedhastane').attr('disabled', true);
            $('#selectedbolum').attr('disabled', true);
            $('#selecteddoktor').attr('disabled', true);
            $('#selectedgun').attr('disabled', true);
            $('#selectedsaat').attr('disabled', true);
            $('#selectedhastane').append('<option>--Hastane secin--</option>');
            $('#selectedbolum').append('<option>--Bolum secin--</option>');
            $('#selecteddoktor').append('<option>--Doktor secin--</option>');
            $('#selectedgun').append('<option>--Gun secin--</option>');
            $('#selectedsaat').append('<option>--Saat secin--</option>');
        }
    });

    $('#selectedgun').change(function () {
        var dcsmId = $(this).val();

        if (dcsmId > 0) {
            Loadsaatler(dcsmId);
        }
        else {
            alert("Tarih Secin!");
            $('#selectedhastane').empty();
            $('#selectedbolum').empty();
            $('#selecteddoktor').empty();
            $('#selectedgun').empty();
            $('#selectedsaat').empty();
            $('#selectedhastane').attr('disabled', true);
            $('#selectedbolum').attr('disabled', true);
            $('#selecteddoktor').attr('disabled', true);
            $('#selectedgun').attr('disabled', true);
            $('#selectedsaat').attr('disabled', true);
            $('#selectedhastane').append('<option>--Hastane secin--</option>');
            $('#selectedbolum').append('<option>--Bolum secin--</option>');
            $('#selecteddoktor').append('<option>--Doktor secin--</option>');
            $('#selectedgun').append('<option>--Gun secin--</option>');
            $('#selectedsaat').append('<option>--Saat secin--</option>');
        }
    });




});


function LoadSehirler() {
    $('#selectedsehir').empty();

    $.ajax({
        url: '/Randevu/GetSehir',
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#selectedsehir').attr('disabled', false);
                $('#selectedsehir').append('<option>--Sehir secin--</option>');
                $('#selectedilce').append('<option>--Ilce secin--</option>');
                $('#selectedhastane').append('<option>--Hastane secin--</option>');
                $('#selectedbolum').append('<option>--Bolum secin--</option>');
                $('#selecteddoktor').append('<option>--Doktor secin--</option>');
                $('#selectedgun').append('<option>--Gun secin--</option>');
                $('#selectedsaat').append('<option>--Saat secin--</option>');
                $.each(response, function (i, data) {
                    $('#selectedsehir').append('<option value=' + data.sehirId + '>' + data.sehirAdi + '</option>');
                    console.log(i.SehirId, i.SehirAdi,i,data);
                });
            }
            else {
                $('#selectedsehir').attr('disabled', true);
                $('#selectedilce').attr('disabled', true);
                $('#selectedhastane').attr('disabled', true);
                $('#selectedbolum').attr('disabled', true);
                $('#selecteddoktor').attr('disabled', true);
                $('#selectedgun').attr('disabled', true);
                $('#selectedsaat').attr('disabled', true);
                $('#selectedsehir').append('<option>--Sehir yok--</option>');
                $('#selectedilce').append('<option>--Ilçe yok--</option>');
                $('#selectedhastane').append('<option>--Hastane yok--</option>');
                $('#selectedbolum').append('<option>--Bolum yok--</option>');
                $('#selecteddoktor').append('<option>--Doktor yok--</option>');
                $('#selectedgun').append('<option>--Gun yok--</option>');
                $('#selectedsaat').append('<option>--Saat yok--</option>');
            }
        },
        error: function (error) {
            alert(error);
        }

    });
}

function Loadilceler(ilceidsi) {
    $('#selectedilce').empty();
    $('#selectedhastane').empty();
    $('#selectedbolum').empty();
    $('#selecteddoktor').empty();
    $('#selectedgun').empty();
    $('#selectedsaat').empty();
    $('#selectedhastane').attr('disabled', true);
    $('#selectedbolum').attr('disabled', true);
    $('#selecteddoktor').attr('disabled', true);
    $('#selectedgun').attr('disabled', true);
    $('#selectedsaat').attr('disabled', true);

    $.ajax({
        url: '/Randevu/Getilce?id=' + ilceidsi,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#selectedilce').attr('disabled', false);
                $('#selectedilce').append('<option>--Ilce secin--</option>');
                $('#selectedhastane').append('<option>--Hastane secin--</option>');
                $('#selectedbolum').append('<option>--Bolum secin--</option>');
                $('#selecteddoktor').append('<option>--Doktor secin--</option>');
                $('#selectedgun').append('<option>--Gun secin--</option>');
                $('#selectedsaat').append('<option>--Saat secin--</option>');
                $.each(response, function (i, data) {
                    $('#selectedilce').append('<option value=' + data.ilceid + '>' + data.ilceAdi + '</option>');
                    console.log(i, data);
                });
            }
            else {
                $('#selectedilce').attr('disabled', true);
                $('#selectedhastane').attr('disabled', true);
                $('#selectedbolum').attr('disabled', true);
                $('#selecteddoktor').attr('disabled', true);
                $('#selectedgun').attr('disabled', true);
                $('#selectedsaat').attr('disabled', true);
                $('#selectedilce').append('<option>--Ilçe yok--</option>');
                $('#selectedhastane').append('<option>--Hastane yok--</option>');
                $('#selectedbolum').append('<option>--Bolum yok--</option>');
                $('#selecteddoktor').append('<option>--Doktor yok--</option>');
                $('#selectedgun').append('<option>--Gun yok--</option>');
                $('#selectedsaat').append('<option>--Saat yok--</option>');
            }
        },
        error: function (error) {
            alert(error);
        }

    });
}


function Loadhastaneler(sehiridsi) {
    $('#selectedhastane').empty();
    $('#selectedbolum').empty();
    $('#selecteddoktor').empty();
    $('#selectedgun').empty();
    $('#selectedsaat').empty();
    $('#selectedbolum').attr('disabled', true);
    $('#selecteddoktor').attr('disabled', true);
    $('#selectedgun').attr('disabled', true);
    $('#selectedsaat').attr('disabled', true);

    $.ajax({
        url: '/Randevu/GetHastane?id=' + sehiridsi,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#selectedhastane').attr('disabled', false);
                $('#selectedhastane').append('<option>--Hastane secin--</option>');
                $('#selectedbolum').append('<option>--Bolum secin--</option>');
                $('#selecteddoktor').append('<option>--Doktor secin--</option>');
                $('#selectedgun').append('<option>--Gun secin--</option>');
                $('#selectedsaat').append('<option>--Saat secin--</option>');
                $.each(response, function (i, data) {
                    $('#selectedhastane').append('<option value=' + data.hastaneId + '>' + data.hastaneAdi + '</option>');
                    console.log(i, data);
                });
            }
            else {
                $('#selectedhastane').attr('disabled', true);
                $('#selectedbolum').attr('disabled', true);
                $('#selecteddoktor').attr('disabled', true);
                $('#selectedgun').attr('disabled', true);
                $('#selectedsaat').attr('disabled', true);
                $('#selectedhastane').append('<option>--Hastane yok--</option>');
                $('#selectedbolum').append('<option>--Bolum yok--</option>');
                $('#selecteddoktor').append('<option>--Doktor yok--</option>');
                $('#selectedgun').append('<option>--Gun yok--</option>');
                $('#selectedsaat').append('<option>--Saat yok--</option>');
            }
        },
        error: function (error) {
            alert(error);
        }

    });
}


function Loadbolumler() {
    $('#selectedbolum').empty();
    $('#selecteddoktor').empty();
    $('#selectedgun').empty();
    $('#selectedsaat').empty();
    $('#selecteddoktor').attr('disabled', true);
    $('#selectedgun').attr('disabled', true);
    $('#selectedsaat').attr('disabled', true);
    $.ajax({
        url: '/Randevu/GetBolum' ,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#selectedbolum').attr('disabled', false);
                $('#selectedbolum').append('<option>--Bolum secin--</option>');
                $('#selecteddoktor').append('<option>--Doktor secin--</option>');
                $('#selectedgun').append('<option>--Gun secin--</option>');
                $('#selectedsaat').append('<option>--Saat secin--</option>');
                $.each(response, function (i, data) {
                    $('#selectedbolum').append('<option value=' + data.bolumId + '>' + data.bolumAdi + '</option>');
                    console.log(i, data);
                });
            }
            else {
                $('#selectedbolum').attr('disabled', true);
                $('#selecteddoktor').attr('disabled', true);
                $('#selectedgun').attr('disabled', true);
                $('#selectedsaat').attr('disabled', true);
                $('#selectedbolum').append('<option>--Bolum yok--</option>');
                $('#selecteddoktor').append('<option>--Doktor yok--</option>');
                $('#selectedgun').append('<option>--Gun yok--</option>');
                $('#selectedsaat').append('<option>--Saat yok--</option>');
            }
        },
        error: function (error) {
            alert(error);
        }

    });
}


function Loaddoktorlar(bolumidsi,hastaneidsi) {
    $('#selecteddoktor').empty();
    $('#selectedgun').empty();
    $('#selectedsaat').empty();
    $('#selectedgun').attr('disabled', true);
    $('#selectedsaat').attr('disabled', true);
    $.ajax({
        url: '/Randevu/GetDoktor?bolumid=' + bolumidsi + '&&hastaneid=' + hastaneidsi,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#selecteddoktor').attr('disabled', false);
                $('#selecteddoktor').append('<option>--Doktor secin--</option>');
                $('#selectedgun').append('<option>--Gun secin--</option>');
                $('#selectedsaat').append('<option>--Saat secin--</option>');
                $.each(response, function (i, data) {
                    $('#selecteddoktor').append('<option value=' + data.doktorId + '>' + data.doktorAdi + '</option>');
                    console.log(i, data);
                });
            }
            else {
                $('#selecteddoktor').attr('disabled', true);
                $('#selectedgun').attr('disabled', true);
                $('#selectedsaat').attr('disabled', true);
                $('#selecteddoktor').append('<option>--Doktor yok--</option>');
                $('#selectedgun').append('<option>--Gun yok--</option>');
                $('#selectedsaat').append('<option>--Saat yok--</option>');
     
            }
        },
        error: function (error) {
            alert(error);
        }

    });
}

function Loadgunler(doktoridsi) {  
    $('#selectedgun').empty();
    $('#selectedsaat').empty();
    $('#selectedsaat').attr('disabled', true);
    $.ajax({
        url: '/Randevu/GetGun?doktorid=' + doktoridsi,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#selectedgun').attr('disabled', false);              
                $('#selectedgun').append('<option>--Gun secin--</option>');
                $('#selectedsaat').append('<option>--Saat secin--</option>');
                $.each(response, function (i, data) {
                    $('#selectedgun').append('<option value=' + data.dcsmId + '>' + data.dcsMcalismaTarihi + '</option>');
                    console.log(i, data);
                });
            }
            else {              
                $('#selectedgun').attr('disabled', true);               
                $('#selectedsaat').attr('disabled', true);               
                $('#selectedgun').append('<option>--Gun yok--</option>');
                $('#selectedsaat').append('<option>--Saat yok--</option>');

            }
        },
        error: function (error) {
            alert(error);
        }

    });
}
function Loadsaatler(dcsmId) {
    $('#selectedsaat').empty();  
    $.ajax({
        url: '/Randevu/GetSaat?dcsmId=' + dcsmId,
        success: function (response) {
            if (response != null && response != undefined && response.length > 0) {
                $('#selectedsaat').attr('disabled', false);             
                $('#selectedsaat').append('<option>--Saat secin--</option>');
                $.each(response, function (i, data) {
                    $('#selectedsaat').append('<option value="' + data + '">' + data + '</option>');
                    console.log(i, data,);
                    
                });
            }
            else {
              
                $('#selectedsaat').attr('disabled', true);
                $('#selectedsaat').append('<option>--Saat yok--</option>');

            }
        },
        error: function (error) {
            alert(error);
        }

    });
}