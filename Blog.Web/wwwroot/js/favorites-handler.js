$(document).ready(function () {
    $('.toggle-favorite').on('click', function () {
        const action = $(this).data('action');
        const toolId = $(this).data('id');
        const token = $('input[name="__RequestVerificationToken"]').val();
        const button = $(this); // Store the button element in a variable

        $.ajax({
            type: "POST",
            url: `/Users/${action === 'add' ? 'favorites/add' : 'favorites/remove'}/${toolId}`,
            headers: {
                RequestVerificationToken: token
            },
            success: function (data) {
                if (action === 'add') {
                    console.log("Successfully added to favorites.");
                    // Update the button and toggle the action
                    button.data('action', 'remove').text('Remove Favorite').removeClass('btn-success').addClass('btn-danger');
                } else {
                    console.log("Successfully removed from favorites.");
                    // Update the button and toggle the action
                    button.data('action', 'add').text('Add Favorite').removeClass('btn-danger').addClass('btn-success');
                }

                // Update the IsFavorite value from the response data
                if (data.IsFavorite) {
                    button.addClass('btn-danger');
                } else {
                    button.addClass('btn-success');
                }
            },
            error: function () {
                alert('You are not logged in!');
            }
        });
    });
});