# ClipboardMonitor



- Fun little windows app created in a hour to monitor clipboard history. 


- runs from a *Dispatch Timer* set to run every few seconds 

```
DispatcherTimer _timer;
 _timer = new DispatcherTimer();
_timer.Tick += new EventHandler(AddToList_Tick);
_timer.Interval = new TimeSpan(0, 0, 2); // every 2 secs.
_timer.Start();
```

- and stores past results in a Queue

```
 private void AddToList_Tick(object sender, EventArgs e)
{
    var item = Clipboard.GetText();

    if (item != "" && item != null && !clipboardQueue.Contains(item))
    {
        clipboardQueue.Enqueue(item);
    }

    if (clipboardQueue.Count > 5)
    {
        clipboardQueue.Dequeue();
    }

    ClipBoardListView.ItemsSource = clipboardQueue.Reverse().ToList();
}
```

- Added a method to copy to the clipboard with a double-click

```
private void CopyContent_DoubleClick(object sender, MouseButtonEventArgs e)
{
    string copiedItem = sender as string;

    if (copiedItem != null)
    {
        Clipboard.SetDataObject(copiedItem);
    }
}
```
