import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Movie } from "../core/models/movie.model";
import { AuthenService } from "../core/services/auth.service";
import { MovieService } from "../core/services/movie.service";
import { UserService } from "../core/services/user.service";

@Component({
    selector: 'app-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css']
})

export class MainComponent implements OnInit {
    private user_id : string =""
    public movieModel : Movie
    public nameUser : string =""
    public numberScroll : number = 0;
    constructor(private authService: AuthenService, private router: Router, private movieService: MovieService,
        private userService: UserService) {
            this.movieModel = new Movie();
    }
    
    ngOnInit(): void {
        this.loadMovies();
        this.loadUserProfile();
    }
    async logoutSubmit()
    {
        this.user_id = localStorage.getItem("user_id") as string;
        await this.authService.logout(this.user_id).subscribe(
            (result: any) => {
                localStorage.removeItem("access_token");
                localStorage.removeItem("user_id");
                this.router.navigateByUrl("/login", { skipLocationChange: true });
            }
        )
    }
    async loadMovies()
    {
        this.user_id = localStorage.getItem("user_id") as string;
        await this.movieService.getRandomMovie().subscribe(async (result:any)=>{
            if(this.movieModel.Id == result.id)
                this.loadMovies()
            this.movieModel.Id = result.id
            this.movieModel.Name = result.name;
            this.movieModel.PathFile = result.pathFile;
            this.movieModel.LikeNumber = result.likeNumber;
            this.movieModel.Priority = result.priority;
            await this.movieService.getReaction(this.movieModel.Id, this.user_id).subscribe((rs:any)=>{
                if(rs != null)
                    this.movieModel.Reaction = rs.status
                else
                    this.movieModel.Reaction = 0
                })
        });
    }
    async loadUserProfile()
    {        
        this.user_id = localStorage.getItem("user_id") as string;
        await this.userService.getUserProfile(this.user_id).subscribe((result:any)=>{
            this.nameUser = result.lastName
        })
    }
    async likeMovie(movieId:string)
    {
        this.user_id = localStorage.getItem("user_id") as string;
        await this.movieService.createReaction(movieId, this.user_id, 1).subscribe(async (result:any)=>{
            await this.reloadMovie(movieId)
        })
    }
    async dislikeMovie(movieId:string)
    {
        this.user_id = localStorage.getItem("user_id") as string;
        await this.movieService.removeReaction(movieId, this.user_id, 0).subscribe(async (result:any)=>{
            await this.reloadMovie(movieId)
        })

    }
    async reloadMovie(movieID:string)
    {
        this.user_id = localStorage.getItem("user_id") as string;
        await this.movieService.getDetailMovie(movieID).subscribe(async (result:any)=>{
            this.movieModel.LikeNumber = result.likeNumber;
            await this.movieService.getReaction(this.movieModel.Id, this.user_id).subscribe((rs:any)=>{
                if(rs != null)
                    this.movieModel.Reaction = rs.status
                else
                    this.movieModel.Reaction = 0
            })
        });
    }
}