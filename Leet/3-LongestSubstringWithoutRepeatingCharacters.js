
/**
 * Given a string s, find the length of the longest substring without duplicate characters.

 

Example 1:

Input: s = "abcabcbb"
Output: 3
Explanation: The answer is "abc", with the length of 3.
Example 2:

Input: s = "bbbbb"
Output: 1
Explanation: The answer is "b", with the length of 1.
Example 3:

Input: s = "pwwkew"
Output: 3
Explanation: The answer is "wke", with the length of 3.
Notice that the answer must be a substring, "pwke" is a subsequence and not a substring.
 

Constraints:

0 <= s.length <= 5 * 104
s consists of English letters, digits, symbols and spaces.
 */


const lengthOfLongestSubstring = (s) => {
    let maxLength = 0;
    let start = 0;
    const charMap = new Map();

    for (let end = 0; end < s.length; end++) {
        const char = s[end];
        // If the character is in the map, it's a duplicate.
        // We move the start of our window past the last seen index of that character.
        if (charMap.has(char)) {
            start = Math.max(start, charMap.get(char) + 1);
        }
        
        // Update the map with the current character and its index.
        charMap.set(char, end);

        // Calculate the length of the current substring and update maxLength.
        const x = end - start + 1;
        maxLength = Math.max(maxLength, x);
    }

    return maxLength;
};


//==================
const lengthOfLongestSubstring1 = (s) => {
    let left = 0;
    let maxLength = 0;
    const map = new Map();

    for(let right = 0; right < s.length; right++) {
        let rightChar = s[right];
        if(map.has(rightChar)) {
            left = Math.max(left, map.get(rightChar) + 1);
        }
        map.set(rightChar, right);
        maxLength = Math.max(maxLength, right - left + 1);
    }
    return maxLength;
}


// Example Usage:
const samples = ["abcbacbb", "abcabcbb", "bbbbb", "pwwkew", "", " "];
for (const sample of samples) {
    console.log(`${sample}: `, lengthOfLongestSubstring1(sample));
}