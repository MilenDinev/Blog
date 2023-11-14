$(document).ready(function () {
    $('#upVoteButton, #downVoteButton').on('click', function () {
        const isUpVote = $(this).attr('id') === 'upVoteButton';
        const toolId = toolData.id

        // Disable both vote buttons during the AJAX request
        $('#upVoteButton, #downVoteButton').prop('disabled', true);

        $.ajax({
            type: 'GET',
            url: `/Users/Vote/${toolId}`,
            data: { isUpVote: isUpVote },
            success: function (response) {
                if (response.success) {
                    updateVoteCount(isUpVote, response);
                    toggleButtonStyles(isUpVote);
                } else {
                    alert('Voting failed.');
                }
                // Re-enable both vote buttons
                $('#upVoteButton, #downVoteButton').prop('disabled', false);
            },
            error: function () {
                alert('You are not logged in!');
                // Re-enable both vote buttons
                $('#upVoteButton, #downVoteButton').prop('disabled', false);
            }
        });
    });

    function updateVoteCount(isUpVote, response) {
        $('#upVotes').text(response.upVotes);
        $('#downVotes').text(response.downVotes);
    }

    function toggleButtonStyles(isUpVote) {
        if (isUpVote) {
            $('#upVoteButton').toggleClass('btn-success btn-outline-success');
            $('#downVoteButton').removeClass('btn-danger').addClass('btn-outline-danger');
        } else {
            $('#downVoteButton').toggleClass('btn-danger btn-outline-danger');
            $('#upVoteButton').removeClass('btn-success').addClass('btn-outline-success');
        }
    }
});