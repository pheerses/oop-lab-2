document.addEventListener("DOMContentLoaded", function () {
    const audio = document.getElementById("audio");
    const playPauseButton = document.getElementById("play-pause");
    const progressContainer = document.getElementById("progress-container");
    const progress = document.getElementById("progress");
    const timeDisplay = document.getElementById("time");

    // Toggle play/pause
    playPauseButton.addEventListener("click", function () {
        if (audio.paused) {
            audio.play();


            audioBlocks[currentIndex].querySelectorAll(".undercover")[0].style.opacity = "0.9";
            playPauseButton.textContent = "---";
        } else {
            audioBlocks[currentIndex].querySelectorAll(".undercover")[0].style.opacity = "0.0";
            audio.pause();
            playPauseButton.textContent = "---";
        }
    });

    // Update progress bar and time display
    audio.addEventListener("timeupdate", function () {
        const currentTime = formatTime(audio.currentTime);
        const duration = formatTime(audio.duration);

        timeDisplay.textContent = `${currentTime} / ${duration}`;

        const progressPercent = (audio.currentTime / audio.duration) * 100;
        progress.style.width = `${progressPercent}%`;
    });

    // Seek functionality by clicking on progress bar
    progressContainer.addEventListener("click", function (e) {
        const width = this.clientWidth;
        const clickX = e.offsetX;
        const duration = audio.duration;

        audio.currentTime = (clickX / width) * duration;
    });

    // Helper function to format time
    function formatTime(time) {
        const minutes = Math.floor(time / 60);
        const seconds = Math.floor(time % 60);
        return `${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;
    }


    // Set initial time display
    audio.addEventListener("loadedmetadata", function () {
        const duration = formatTime(audio.duration);
        timeDisplay.textContent = `00:00 / ${duration}`;
    });

    const audioPlayer = document.getElementById("audio");
    const audioBlocks = document.querySelectorAll(".track-row-cover");
    let currentlyPlaying = null;
    let currentIndex = -1; // Индекс текущего трека

    audioBlocks.forEach((block, index) => {
        block.addEventListener("click", function () {
            if (currentIndex != -1) {

                audioBlocks[currentIndex].querySelectorAll(".undercover")[0].style.opacity = "0.0";
            }
            const trackPath = this.parentElement.getAttribute("track-path");
            if (currentlyPlaying === trackPath) {
                // Если текущий аудиофайл уже воспроизводится, ставим его на паузу
                if (audioPlayer.paused) {

                    this.querySelectorAll(".undercover")[0].style.opacity = "0.9";
                    audioPlayer.play();
                } else {

                    this.querySelectorAll(".undercover")[0].style.opacity = "0.0";
                    audioPlayer.pause();
                }
            } else {
                // Если выбран новый аудиофайл
                audioPlayer.src = trackPath;
                audioPlayer.play();
                currentlyPlaying = trackPath;
                currentIndex = index; // Устанавливаем текущий индекс

                this.querySelectorAll(".undercover")[0].style.opacity = "0.9";
            }
        });
    });
    audioPlayer.addEventListener("ended", function () {
        console.log("sss")
        // Переключение на следующий трек, если он есть
        if (currentIndex < audioBlocks.length - 1) {


            audioBlocks[currentIndex].querySelectorAll(".undercover")[0].style.opacity = "0.0";
            currentIndex++;


            audioBlocks[currentIndex].querySelectorAll(".undercover")[0].style.opacity = "0.9";
            const nextTrackPath = audioBlocks[currentIndex].parentElement.getAttribute("track-path");
            audioPlayer.src = nextTrackPath;
            audioPlayer.play();
            currentlyPlaying = nextTrackPath;
        } else {
            currentIndex = -1; // Сбросить индекс, если достигнут конец списка
        }
    });

    // Переключение на предыдущий трек
    document.getElementById("prev-button").addEventListener("click", function () {
        if (currentIndex > 0) {
            currentIndex--; // Уменьшаем индекс
            const prevTrackPath = audioBlocks[currentIndex].parentElement.getAttribute("track-path");
            audioPlayer.src = prevTrackPath;
            audioPlayer.play();
            currentlyPlaying = prevTrackPath;
        }
    });

    // Переключение на следующий трек
    document.getElementById("next-button").addEventListener("click", function () {
        if (currentIndex < audioBlocks.length - 1) {
            currentIndex++; // Увеличиваем индекс
            const nextTrackPath = audioBlocks[currentIndex].parentElement.getAttribute("track-path");
            audioPlayer.src = nextTrackPath;
            audioPlayer.play();
            currentlyPlaying = nextTrackPath;
        }
    });

    // Обработчик окончания воспроизведения
    
});