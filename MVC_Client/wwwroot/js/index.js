function showUsers() {

		var currentLookupName = $('#cusId').val();

		$.ajax({
	type: 'POST',
			url: 'customers/GetUsers',
			data: {currentLookupName: currentLookupName },
			success: function (response) {
				var table = $('.table-data');
				table.empty();
				table.append(
					'<div class="row fields-borders">' +
					'<div class="col-sm-4 title-font right-border">' +
					'	<span>Id</span>' +
					'</div>' +
					'<div class="col-sm-4 title-font right-border">' +
					'	<span>Username</span>' +
					'</div>' +
					'<div class="col-sm-4 title-font">' +
					'	<span>Number of products</span>' +
					'</div>' +
					'</div>'
				)

				for (var i = 0; i < response.length; i++) {
					var name = "'" + response[i].name + "'";
					table.append(
						'<div class="row fields-borders mark-pointed" onclick="showSingleUserPage(' + response[i].id + ',' + name + ')"> ' +
						'	<div class="col-sm-4 right-border">' +
						response[i].id +
						'	</div> ' +
						'	<div class="col-sm-4 right-border">' +
						response[i].name +
						'	</div> ' +
						'	<div class="col-sm-4"> ' +
						response[i].numberOfProducts +
						'	</div>' +
						'</div>'
					)
				}
				$('#searcher').fadeIn(200);
			},
			error: function (e) {
	alert(e);
}
		});
	};

	function showSingleUserPage(userId, name) {
	$('#searcher').fadeOut(200);
$.ajax({
	type: 'POST',
			url: 'customers/ShowUserData',
			data: {
	userId: userId,
				name: name
			},
			success: function (response) {
				var table = $('.table-data');
				table.empty();

				table.append(
					'<div class="row fields-borders">' +
					'<div class="col-sm-4 title-font right-border">' +
					'	<span>Id</span>' +
					'</div>' +
					'<div class="col-sm-4 title-font right-border">' +
					'	<span>Username</span>' +
					'</div>' +
					'<div class="col-sm-4 title-font">' +
					'	<span>Number of orders</span>' +
					'</div>' +
					'</div>'
				);

				table.append(
					'<div class="row fields-borders"> ' +
					'	<div class="col-sm-4 right-border">' +
					response.id +
					'	</div> ' +
					'	<div class="col-sm-4 right-border">' +
					response.name +
					'	</div> ' +
					'	<div class="col-sm-4"> ' +
					response.orderList.length +
					'	</div>' +
					'</div>'
				);

				table.append(
					'<div class="row fields-borders">' +
					'	<div class="col-sm-12 orders-title"> ' +
					'		<span>ORDERS</span>' +
					'	</div>' +
					'</div>' +
					'<div class="row fields-borders">' +
					'<div class="col-sm-3 title-font right-border">' +
					'	<span>Order name</span>' +
					'</div>' +
					'<div class="col-sm-3 title-font right-border">' +
					'	<span>Total</span>' +
					'</div>' +
					'<div class="col-sm-3 title-font right-border">' +
					'	<span>Number of products</span>' +
					'</div>' +
					'<div class="col-sm-3 title-font">' +
					'	<span>Possible problems</span>' +
					'</div>' +
					'</div>'
				);

				var orderList = response.orderList;

				for (var i = 0; i < orderList.length; i++) {
					var possibleIssue;
					if (orderList[i].discontinued) {
	possibleIssue = "Discontinued";
} else if (orderList[i].productsInStock < orderList[i].numberOfProducts) {
	possibleIssue = "No products in stock";
} else {
	possibleIssue = "No";
}

					table.append(
						'<div class="row fields-borders">' +
						'	<div class="col-sm-3 right-border">' +
						orderList[i].name +
						'	</div> ' +
						'	<div class="col-sm-3 right-border">' +
						orderList[i].sum +
						'	</div> ' +
						'	<div class="col-sm-3 right-border"> ' +
						orderList[i].numberOfProducts +
						'	</div>' +
						'	<div class="col-sm-3"> ' +
						'     <span>' + possibleIssue + '</span>' +
						'	</div>' +
						'</div>'
					)
				}

				table.append(
					'<div class="row fields-borders" onclick="showUsers()">' +
					'	<div class="col-sm-12 go-back-button"> ' +
					'		<span>GO BACK</span>' +
					'	</div>' +
					'</div>'
				);

			},
			error: function (e) {
	alert(e);
}
		})
	}

	function appendPossibleIssue(issue) {

}

