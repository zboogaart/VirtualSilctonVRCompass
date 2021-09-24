function Update() {
    // Slowly rotate the object arond its X axis at 1 degree/second.
    transform.Rotate(Time.deltaTime, 0.5, 1);

    // ... at the same time as spinning it relative to the global 
    // Y axis at the same speed.
    transform.Rotate(0.5, Time.deltaTime, 1, Space.World);
}