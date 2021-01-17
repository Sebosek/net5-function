namespace Function.Extensions
{
    public static class StringExtensions
    {
        public static string WithLogo(this string text) =>
            $@"
 ,-----.                       ,------.                ,---.   
'  .-.  ' ,---.  ,---. ,--,--, |  .---',--,--. ,--,--.'   .-'  
|  | |  || .-. || .-. :|      \|  `--,' ,-.  |' ,-.  |`.  `-.  
'  '-'  '| '-' '\   --.|  ||  ||  |`  \ '-'  |\ '-'  |.-'    | 
 `-----' |  |-'  `----'`--''--'`--'    `--`--' `--`--'`-----'  
         `--'                                                  
                                         {text}";
    }
}