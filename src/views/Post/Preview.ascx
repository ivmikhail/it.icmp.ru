<%@ Control Language="C#" Inherits="ViewUserControl<PostEditModel>" %>


<h1>
    <%= Model.ToPost().TitleFormatted %>
    <span class="meta">не сохранено</span>
</h1>

<div class="text">

    <%= Model.ToPost().DescriptionFormatted %>

    <hr id="cut" />

    <%= Model.ToPost().TextFormatted %>

</div>
