@code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Leave Summary Calendar</h2>

<div id="calender"></div>

<div id="myModal" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title"><span id="eventTitle"></span></h4>
            </div>
            <div class="modal-body">
                <p id="pDetails"></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

<link href="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.min.css" rel="stylesheet" />
<link href="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.print.css" rel="stylesheet" media="print" />

@section scripts
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.4.0/fullcalendar.min.js"></script>

    <script>
        $(document).ready(function () {
            var events = [];
            $.ajax({
                type: "GET",
                url: "LoadHtml/GetEvents",
                success: function (data) {
                    $.each(data, function (i, v) {
                        events.push({
                            title: v.USER_NAME,
                            description: v.LEAVE_TYPE,
                            start: moment(v.LEAVE_FROM,"DD-MM-YYYY"),
                            end: v.LEAVE_TO != null ? moment(v.LEAVE_TO, "DD-MM-YYYY") : null,
                            fromSection: v.LEAVE_FROM_AMPM,
                            toSection: v.LEAVE_TO_AMPM
                        });
                    })

                    GenerateCalendar(events);
                },
                error: function (error) {
                    alert('Leaves Summary Calendar generation is failed');
                }
            });

            function GenerateCalendar(events) {
                $('#calender').fullCalendar('destroy');
                $('#calender').fullCalendar({
                    contentHeight: 600,
                    defaultDate: new Date(),
                    timeFormat: 'h(:mm)a',
                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: null //'month,basicWeek,basicDay,agenda'
                    },
                    weekends: true,
                    //hiddenDays: [0, 6],
                    showNonCurrentDates: false,
                    displayEventTime: false,
                    eventLimit: true,
                    eventColor: '#2196F3',
                    events: events,
                    eventClick: function (calEvent, jsEvent, view) {
                        $('#myModal #eventTitle').text(calEvent.title);
                        var endDay = calEvent.end;

                        var $description = $('<div/>');
                        $description.append($('<p/>').html('<b>Start:</b>' + calEvent.start.format("DD-MM-YYYY") + '(' + calEvent.fromSection + ')'));
                        if (calEvent.end != null) {
                            $description.append($('<p/>').html('<b>End:</b>' + endDay.subtract(1,'days').format("DD-MM-YYYY") + '(' + calEvent.toSection + ')'));
                        }
                        $description.append($('<p/>').html('<b>Description:</b>' + calEvent.description));

                        endDay = calEvent.end.add(1, 'days'); //to display same day after minus 1 day, need to add back 1 day

                        $('#myModal #pDetails').empty().html($description);

                        $('#myModal').modal();
                    }
                })
            }


      });
    </script>
End Section

