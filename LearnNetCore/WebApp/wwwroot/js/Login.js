function myLogin() {
	var validate = new Object();
	validate.Email = $('#Username').val();
	validate.Password = $('#password').val();
	//console.log(validate);
	$.ajax({
		type: 'POST',
        url: "/validate/",
		cache: false,
		dataType: "JSON",
		data: validate
    }).then((result) => {
        if (result.status == true) {
			window.location.href = "/dashboard";
		} else {
			toastr.warning(result.msg)
		}
	})
};

function Register() {
    var confirm = $("#confirmPassword").val();
    var pw = $("#password").val();
    if (confirm == pw) {
        var dataRegister = {
            username: $("#username").val(),
            email: $("#email").val(),
            password: $("#password").val(),
            phone: $("#phone").val(),
            confirmPassword: $("#confirmPassword").val()
        };
        console.log(dataRegister);
        $.ajax({
            type: 'POST',
            url: "/regisvalidate/",
            cache: false,
            dataType: "JSON",
            data: dataRegister
        }).then((result) => {
            if (result.status == true) {
                toastr.success("Please check your email to continue your registration proccess.")
                window.location.href = "/verify";
            } else {
                toastr.warning(result.msg)
            }
            console.log(result);
        })
    } else {
        //toastr.warning(result.msg)
    }
};

function Verify() {
    var validate = {
        SecurityStamp : $('#verifyId').val(),
        Email: $('#email').val()
    };
    console.log(validate);
    $.ajax({
        type: 'POST',
        url: "/verif/",
        cache: false,
        dataType: "JSON",
        data: validate
    }).then((result) => {
        //window.location.href = "/dashboard";
        if (result.status == true) {
            window.location.href = "/dashboard";
        } else {
            toastr.warning(result.msg)
        }
        console.log(result);
    });
}