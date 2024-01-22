# UnityRandomManager

The Random manager class is a singelton with the purpose to help to handle multiple random seeds.

I used partial class to make it modulable, for exemple, if you don't need the save/load part, just remove the file.

the main RandomItem is always accessible under the key "main"
> RandomManager.Instance.item["main"]

## RandomItem:

A level of abstraction over the random class to make it easier to use.

> RandomItem() -> constructor
>
>> generate a seed based of the current unix timestamp DateTimeOffset.Now.ToUnixTimeSeconds() 
>>
>
>> Initialise a new random based on the seed

 > RandomItem(int seed) -> constructor
 >
 >> set the seed with the one passed on parameter
 >
 >> generate a new random item based on the seed

 > int Seed -> accessor
 >
 >> Get: return the seed value

 > Random Random -> accessor
 >
 >> return the Random item

## RandomManager.cs:

The core part of the RandomManager class, store a dict of RandomItem and give access to them by key.

> static Instance -> accessor
>
>> If it not exists, initialise a new instance of Random manager.
>
>> Return the unique instance of the Random manager

> Init(List\<string> keys, bool clear "optional")
>
>> Keys: list of keys used to init the dict of RandomItem
>
>> clear: boolean, true as default, if true the dict is clear before fill it, else add the new keys to the dict

> Add key(string key)
>
>> Add a new entry inside the dict, with key as dict key

> Reset()
>
>> Clear the RandomManager and generate a new entry in the  dict under the "main" key

> Reset(int seed)
>
>> Clear the RandomManager and generate a new entry in the  dict under the "main" key, using the seed to init the RandomItem


## RandomManagerSaving.cs

Utilities function to import and export the RandomManager

> List\<string> Export()
>
>> Generate a string list from the dict under the format (key=seed) for saving purpose

> Import(List\<string> toImport)
>
>> Parse the string list passed as a parameter uder the format (key=seed) to fill the class dict, if the parameter clear is set to false, add the items to the dict