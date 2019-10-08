namespace Template.Services.Services.Contracts
{
    public interface IRngService
    {
        /// <summary>
        /// Generates a random array of bytes.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        byte[] Bytes(int size);
    }
}
