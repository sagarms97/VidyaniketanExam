        function success(msg) {
            //         toastr.options.positionClass = "toast-bottom-left";
            toastr.success(msg);
        }
        function warning(msg) {
            //        toastr.options.timeOut = 1500; // 1.5s
            //        toastr.options.positionClass = "toast-bottom-left";
            toastr.warning(msg);
        }
        function info(msg) {
            //        toastr.options.timeOut = 1500; // 1.5s
            toastr.info(msg);
        }
        function error(msg) {
                toastr.options.timeOut = 1500; // 1.5s
            toastr.error(msg);
        }