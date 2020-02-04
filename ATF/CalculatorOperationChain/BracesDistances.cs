namespace CalculatorOperationChain
{
    public struct BracesDistances
    {
        public  int OpeningBraceIndex;
        public  int ClosingBraceIndex;
        public int Distance;

        public BracesDistances(int openingBraceIndex, int closingBraceIndex, int distance)
        {
            OpeningBraceIndex = openingBraceIndex;
            ClosingBraceIndex = closingBraceIndex;
            Distance = distance;
        }
    }
}