# TextToSpeech-TikTokAPI

This is a small .NET Framework WPF program that uses the TikTok API. Application saves .mp3 or .aac file and can read text from .txt file. Max symbols for one operation: 300.

**Requirements:**
* Windows 10
* Windows 7 with installed .NET Framework 4.6 or newer

How to use?
===========
I. Installation
---------------
1) Download release (TikTokTTS.[version].zip)  
2) Extract all files into one folder (folder can be placed anywhere)   
3) Run "TikTokTTS.exe" 

II. Usage
---------
![TikTokTTS_SJiZH0cMeu](https://user-images.githubusercontent.com/88380021/170978763-4d281720-bbb8-4f23-b3ca-8ec6898ab6c3.png)  

• **Output path** - where the file will be saved. You can write the path yourself (the path must contain the file name and the extension (as in the screenshot)) or click on "•••" to select the path through the explorer.

    
• **Voice** - the voice that will be used to speech your text. 

   Language    
   │     
   `en`_`au`__`001` ─ Variant number   
        │    
        Pronunciation   
        
The first two letters designate the language, the next two letters after the underscore (if they are) designate the pronunciation.
        
• **Text** - is the text that will be converted to speech.

• **`Load from text file`** - you can select .txt file and load text from it    

• Click `Start` to convert text to speech

Languages
=========
| Language                | Voice codes                                                                                  |
| ----------------------- |:--------------------------------------------------------------------------------------------:|
| English (Australia)     | en_au_001, en_au_002                                                                         |
| English (United Kingdom)| en_uk_001, en_uk_003                                                                         |
| English (United States) | en_us_001, en_us_002, en_us_006, en_us_007, en_us_009, en_us_010                             |
| English (Disney)        | en_us_ghostface, en_us_chewbacca, en_us_c3po, en_us_stitch, en_us_stormtrooper, en_us_rocket |
| German                  | de_001, de_002                                                                               |
| Portuguese (Brazil)     | br_001, br_003, br_004, br_005                                                               |
| Spanish (Mexico)        | es_mx_002                                                                                    |
| Korean                  | kr_002, kr_003, kr_004                                                                       |
| Japanese                | jp_001, jp_003, jp_005, jp_006                                                               |
| Spanish                 | es_002                                                                                       |
| French                  | fr_001, fr_002                                                                               |
| Indonesian              | id_001                                                                                       |

History
=======
By default, the program saves all operations to a .json file (path: *C:\Users\\[USER]\AppData\Local\TikTokTTS*).  
On startup the application loads the last session. This feature can be disabled in the settings.

![history](https://user-images.githubusercontent.com/88380021/184322369-90f907bc-e9ac-4494-b82b-e4bb6fc5d462.png)
