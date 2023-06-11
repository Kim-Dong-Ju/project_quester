def solution(arr,query):
    for i in query:
        if i%2 == 0:
            arr=arr[:i+1]
        else:
            arr=arr[i:]
    print(arr)

solution([0,1,2,3,4],[4,1,2])