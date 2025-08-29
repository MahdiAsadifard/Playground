/**48. Rotate Image
 * You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).

You have to rotate the image in-place, which means you have to modify the input 2D matrix directly. DO NOT allocate another 2D matrix and do the rotation.

 

Example 1:


Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
Output: [[7,4,1],[8,5,2],[9,6,3]]
Example 2:


Input: matrix = [[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]
Output: [[15,13,2,5],[14,3,4,1],[12,6,8,9],[16,7,10,11]]
 
 */

// Here we don;t return anything, only change items in original matrix
const rotateInline = (matrix) => {
    const n = matrix[0].length;

    for(let i = 0; i < n; i++){
        const cur = matrix[i];
        for(let j = 0 ; j < cur.length; j++) {
            if(i < j) {
                const tmp = matrix[i][j];
                matrix[i][j] = matrix[j][i];
                matrix[j][i] = tmp;
            }
        }
    }
    for(let i = 0; i < n; i++){
        matrix[i].reverse();
    }
}
const  rotate = function(matrix) {
    const result = [];
    const map = new Map();
    const n = matrix[0].length;

    let counter = 0
    matrix.map((m, i) => {
        m.map((item, j) =>
        {
            map.set(counter++, item); //0:1, 1:2, 2:3 , 3:4, 4:5. 5:6. 6:7, 7:8, 8:9
        })
    });


    for(let i = 0; i < n; i++){
        const tmp = []
        for(let j = i; j < counter; j+=n){
            const x = map.get(j)
            tmp.push(x)
        }
        tmp.reverse();
        result.push(tmp)
    }
console.log(matrix)
console.log(result)
   return result;

};

const samples = [
    [[1,2,3],[4,5,6],[7,8,9]],
    [[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]
];

for (const sample of samples) {
    const result = rotate(sample);
    console.log(`${sample} => ${result}`);
    
    rotateInline(sample);
}
