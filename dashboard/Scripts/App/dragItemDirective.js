mainAppModule.directive('dragItem', ['$document', function ($document) {
    return {
        scope: {
            'obj': '=',
        },
        link: function (scope, element, attr) {
            //scope.obj

            element.css({
                position: 'relative',
                cursor: 'pointer',
                top: scope.obj.positionY + 'px',
                left: scope.obj.positionX + 'px',
                zIndex: scope.obj.zIndex
            });
            
            var startX = scope.obj.positionX;
            var startY = scope.obj.positionY;
            var x = scope.obj.positionX, y = scope.obj.positionY;

            element.attr('draggable', 'true');

            element.on('mousedown', function (event) {
                // Prevent default dragging of selected content
                event.preventDefault();
                startX = event.pageX - x;
                startY = event.pageY - y;
                $document.on('mousemove', mousemove);
                $document.on('mouseup', mouseup);
            });

            function mousemove(event) {
                scope.obj.positionY = event.pageY - startY;
                scope.obj.positionX = event.pageX - startX;
                element.css({
                    top: scope.obj.positionY + 'px',
                    left: scope.obj.positionX + 'px'
                });
            }

            function mouseup() {
                $document.off('mousemove', mousemove);
                $document.off('mouseup', mouseup);
                scope.$emit('updateItem', scope.obj);
            }


            /*
            element.on('dragstart', function (event) {
                console.log(element);
                element[0].style.opacity = 0.4;

                console.log('started');
            });

            element.on('dragend', function (event) {
                element[0].style.opacity = 1;
                console.log('ended');
            });
            */
        }
    }
}]);