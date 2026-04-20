Imports NUnit.Framework

Public Class GildedRoseTests
    <Test>
    Public Sub ExampleTest()
        Dim Items As IList(Of Item) = New List(Of Item) From {
                New Item With {
                .Name = "foo",
                .SellIn = 0,
                .Quality = 0
                }
                }
        Dim app = New GildedRose(Items)
        app.UpdateQuality()
        Assert.AreEqual("fixme", Items(0).Name)
    End Sub
End Class