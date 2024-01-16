namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string WordCount(this string str)
        {
            string firstChar = str.Substring(0,1);
            string scendeChar = str.Substring(1);
            string relts = scendeChar
                .Replace("A", "\nA")
                .Replace("B", "\nB")
                .Replace("C", "\nC")
                .Replace("D", "\nD")
                .Replace("E", "\nE")
                .Replace("F", "\nF")
                .Replace("G", "\nG")
                .Replace("H", "\nH")
                .Replace("I", "\nI")
                .Replace("J", "\nJ")
                .Replace("K", "\nK")
                .Replace("L", "\nL")
                .Replace("N", "\nN")
                .Replace("M", "\nM")
                .Replace("O", "\nO")
                .Replace("P", "\nP")
                .Replace("Q", "\nQ")
                .Replace("R", "\nR")
                .Replace("S", "\nS")
                .Replace("T", "\nT")
                .Replace("U", "\nU")
                .Replace("P", "\nP")
                .Replace("W", "\nW")
                .Replace("X", "\nX")
                .Replace("Y", "\nY")
                .Replace("Z", "\nZ");
            return firstChar + relts;
        }
    }
}