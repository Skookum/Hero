﻿@helper MakeNote(string content) {
  <div class="note" 
       style="border: 1px solid black; width: 90%; padding: 5px; margin-left: 15px;">
    <p>
      <strong>Note</strong>&nbsp;&nbsp; @content
    </p>
  </div>
}
