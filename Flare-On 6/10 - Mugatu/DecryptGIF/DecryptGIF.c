#include <stdio.h>
#include <stdlib.h>
#include <memory.h>
#include <stdint.h>

void decipher(unsigned long *const v, unsigned long *const w, const unsigned long *const k)
{
	register unsigned long       y = v[0], z = v[1], sum = 0xC6EF3720,
		delta = 0x9E3779B9, a = k[0], b = k[1], c = k[2],
		d = k[3], n = 32;

	/* sum = delta<<5, in general sum = delta * n */

	while (n-- > 0)
	{
		z -= (y << 4) + c ^ y + sum ^ (y >> 5) + d;
		y -= (z << 4) + a ^ z + sum ^ (z >> 5) + b;
		sum -= delta;
	}

	w[0] = y; w[1] = z;
}

void decryptData(unsigned int rounds, uint32_t v[2], uint32_t const k[4])
{
	uint32_t v0, v1;
	uint32_t sum;
	const uint32_t delta = 0x9E3779B9;

	v0 = v[0];
	v1 = v[1];
	sum = delta * rounds;
	do {
		v1 -= ((((v0 << 4) ^ (v0 >> 5)) + v0) ^ (k[(sum >> 0xb) & 3] + sum));
		sum -= delta;
		v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (k[sum & 3] + sum);

		rounds--;
	} while (rounds != 0);
	v[0] = v0;
	v[1] = v1;

	return;
}

void long_to_string(uint32_t v[2], unsigned char buffer[8])
{
	buffer[0] = (v[0] >> 0) & 0xff;
	buffer[1] = (v[0] >> 8) & 0xff;
	buffer[2] = (v[0] >> 16) & 0xff;
	buffer[3] = (v[0] >> 24) & 0xff;
	buffer[4] = (v[1] >> 0) & 0xff;
	buffer[5] = (v[1] >> 8) & 0xff;
	buffer[6] = (v[1] >> 16) & 0xff;
	buffer[7] = (v[1] >> 24) & 0xff;
}

int findKey(unsigned char *buffer, uint32_t k[4])
{
	unsigned char result[8];
	char *target = "GIF8";
	int found = 0;
	uint32_t v[2];

	while (found == 0)
	{
		for (k[0]=0x31; k[0] < 256; k[0]++)
		{
			for (k[1]=0x73; k[1] < 256; k[1]++)
			{
				for (k[2]=0x35; k[2] < 256; k[2]++)
				{
					for (k[3]=0xb1; k[3] < 256; k[3]++)
					{
						v[0] = (buffer[3] << 24) | (buffer[2] << 16) | (buffer[1] << 8) | buffer[0];
						v[1] = (buffer[7] << 24) | (buffer[6] << 16) | (buffer[5] << 8) | buffer[4];

						//decipher(v, v, k);
						decryptData(32, v, k);
						long_to_string(v, result);
						found = 1;
						for (int i = 0; i < 4; i++)
						{
							if (result[i] != target[i])
							{
								found = 0;
								break;
							}
						}
						if (found == 1)
							break;
					}
					if (found == 1)
						break;
				}
				if (found == 1)
					break;
			}
			if (found == 1)
				break;
		}
	}

	return found;
}

int main(int argc, char *argv[])
{
	if (argc != 3)
	{
		printf("Usage: %s input_file output_file\n", argv[0]);
		return 1;
	}

	unsigned char buffer[8];
	uint32_t v[2];
	uint32_t key[4];

	FILE *hInputFile, *hOutputFile;
	fopen_s(&hInputFile, argv[1], "rb");
	
	fread(buffer, sizeof(buffer), 1, hInputFile);
	if (findKey(buffer, key) == 0)
	{
		printf("Failed to find key!\n");
		fclose(hInputFile);
		return 2;
	}

	printf("Found key: ");
	for (int i = 0; i < 4; i++)
		printf("%08x", key[i]);
	printf("\n");
	fseek(hInputFile, 0, 0);
	
	//key[0] = 0x31;
	//key[1] = 0x73;
	//key[2] = 0x35;
	//key[3] = 0xb1;
	fopen_s(&hOutputFile, argv[2], "wb");
	do
	{
		memset(buffer, 0, sizeof(buffer));
		fread(buffer, sizeof(buffer), 1, hInputFile);
		v[0] = (buffer[3] << 24) | (buffer[2] << 16) | (buffer[1] << 8) | buffer[0];
		v[1] = (buffer[7] << 24) | (buffer[6] << 16) | (buffer[5] << 8) | buffer[4];
		//decipher(v, v, k);
		decryptData(32, v, key);
		long_to_string(v, buffer);
		fwrite(buffer, sizeof(buffer), 1, hOutputFile);
	} while (!feof(hInputFile));

	fclose(hInputFile);
	fclose(hOutputFile);
	
	return 0;
}
