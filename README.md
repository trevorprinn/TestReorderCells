# TestReorderCells

A test project to demonstrate some problems with re-ordering cells in a ListView in Xamarin.Forms 1.4.2 (Android). 

The app displays some text items in a ListView, together with buttons to re-order the cells. When the buttons are pressed, it is common for one of the items moved to display without the buttons. It looks like the label on the cell, which is set to FillAndExpand, has expanded to fill the entire cell, pushing the buttons off the end. 